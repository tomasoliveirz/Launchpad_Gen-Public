use actix_web::{web, App, HttpResponse, HttpServer, Responder, Error};
use actix_multipart::Multipart;
use futures_util::StreamExt as _;
use std::io::{Write, Seek};
use tempfile::{TempDir};
use std::fs::{self, File};
use std::path::PathBuf;
use std::process::Command;
use zip::ZipArchive;
use serde::{Serialize, Deserialize};
use std::collections::HashMap;

#[derive(Serialize)]
struct CompileResponse {
    message: String,
    logs: String,
    binary: Option<String>,
}

#[derive(Serialize)]
struct StatusResponse {
    status: String,
    version: String,
}

#[derive(Deserialize)]
struct ProjectFiles {
    files: HashMap<String, String>,
}

/// Endpoint to compile a project via multipart file upload.
async fn compile(mut payload: Multipart) -> Result<HttpResponse, Error> {
    // We'll store the uploaded zip file here.
    let mut zip_file_path: Option<(PathBuf, TempDir)> = None;

    // Process multipart fields
    while let Some(item) = payload.next().await {
        let mut field = item?;
        let content_disposition = field.content_disposition();
        if let Some(name) = content_disposition.get_name() {
            if name == "projectZip" {
                // Create a temporary directory to store the uploaded file
                let temp_dir = TempDir::new().map_err(|e| {
                    actix_web::error::ErrorInternalServerError(format!("TempDir error: {}", e))
                })?;
                let file_path = temp_dir.path().join("project.zip");
                let mut f = File::create(&file_path).map_err(|e| {
                    actix_web::error::ErrorInternalServerError(format!("File create error: {}", e))
                })?;

                // Write the field data to the file
                while let Some(chunk) = field.next().await {
                    let data = chunk?;
                    f.write_all(&data)?;
                }
                // Ensure the file pointer is at the beginning
                f.rewind().unwrap();
                zip_file_path = Some((file_path, temp_dir));
            }
        }
    }

    // Ensure a file was uploaded
    let (zip_path, _keep_temp_dir) = if let Some(tuple) = zip_file_path {
        tuple
    } else {
        return Ok(HttpResponse::BadRequest().body("Missing 'projectZip' file"));
    };

    // Create a temporary directory for extraction
    let extract_dir = TempDir::new().map_err(|e| {
        actix_web::error::ErrorInternalServerError(format!("TempDir error: {}", e))
    })?;
    
    // Extract ZIP file
    {
        let file = File::open(&zip_path).map_err(|e| {
            actix_web::error::ErrorInternalServerError(format!("Failed to open ZIP: {}", e))
        })?;
        let mut archive = ZipArchive::new(file).map_err(|e| {
            actix_web::error::ErrorInternalServerError(format!("ZIP error: {}", e))
        })?;

        for i in 0..archive.len() {
            let mut file = archive.by_index(i).map_err(|e| {
                actix_web::error::ErrorInternalServerError(format!("ZIP error: {}", e))
            })?;
            let outpath = extract_dir.path().join(file.sanitized_name());

            if file.is_dir() {
                fs::create_dir_all(&outpath)?;
            } else {
                if let Some(parent) = outpath.parent() {
                    if !parent.exists() {
                        fs::create_dir_all(parent)?;
                    }
                }
                let mut outfile = File::create(&outpath)?;
                std::io::copy(&mut file, &mut outfile)?;
            }
        }
    }

    // Run `anchor build` in the extracted directory.
    let output = Command::new("anchor")
        .arg("build")
        .current_dir(extract_dir.path())
        .output()
        .map_err(|e| {
            actix_web::error::ErrorInternalServerError(format!("Failed to execute anchor build: {}", e))
        })?;

    let stdout_str = String::from_utf8_lossy(&output.stdout).to_string();
    let stderr_str = String::from_utf8_lossy(&output.stderr).to_string();
    let logs = format!("STDOUT:\n{}\nSTDERR:\n{}", stdout_str, stderr_str);

    // Try to locate the compiled binary (.so file) in the target/deploy directory
    let deploy_dir = extract_dir.path().join("target/deploy");
    let mut binary_name = None;
    if deploy_dir.exists() {
        if let Ok(entries) = fs::read_dir(deploy_dir) {
            for entry in entries.flatten() {
                let path = entry.path();
                if let Some(ext) = path.extension() {
                    if ext == "so" {
                        binary_name = path.file_name().and_then(|n| n.to_str()).map(String::from);
                        break;
                    }
                }
            }
        }
    }

    let response = CompileResponse {
        message: "Anchor build completed successfully".to_string(),
        logs,
        binary: binary_name,
    };

    Ok(HttpResponse::Ok().json(response))
}

/// Endpoint to compile a project via a JSON object with file mappings.
async fn compile_json(project: web::Json<ProjectFiles>) -> Result<HttpResponse, Error> {
    // Create a temporary directory to reconstruct the project.
    let temp_dir = TempDir::new().map_err(|e| {
        actix_web::error::ErrorInternalServerError(format!("TempDir error: {}", e))
    })?;

    // Iterate over the file mappings and write them to the appropriate paths.
    for (relative_path, content) in &project.files {
        let file_path = temp_dir.path().join(relative_path);
        if let Some(parent) = file_path.parent() {
            fs::create_dir_all(parent).map_err(|e| {
                actix_web::error::ErrorInternalServerError(format!("Failed to create directory {}: {}", relative_path, e))
            })?;
        }
        fs::write(&file_path, content).map_err(|e| {
            actix_web::error::ErrorInternalServerError(format!("Failed to write file {}: {}", relative_path, e))
        })?;
    }

    // Run `anchor build` in the temporary directory.
    let output = Command::new("anchor")
        .arg("build")
        .current_dir(temp_dir.path())
        .output()
        .map_err(|e| {
            actix_web::error::ErrorInternalServerError(format!("Failed to execute anchor build: {}", e))
        })?;

    let stdout_str = String::from_utf8_lossy(&output.stdout).to_string();
    let stderr_str = String::from_utf8_lossy(&output.stderr).to_string();
    let logs = format!("STDOUT:\n{}\nSTDERR:\n{}", stdout_str, stderr_str);

    // Try to locate the compiled binary (.so file) in the target/deploy directory
    let deploy_dir = temp_dir.path().join("target/deploy");
    let mut binary_name = None;
    if deploy_dir.exists() {
        if let Ok(entries) = fs::read_dir(deploy_dir) {
            for entry in entries.flatten() {
                let path = entry.path();
                if let Some(ext) = path.extension() {
                    if ext == "so" {
                        binary_name = path.file_name().and_then(|n| n.to_str()).map(String::from);
                        break;
                    }
                }
            }
        }
    }

    let response = CompileResponse {
        message: "Anchor build completed successfully".to_string(),
        logs,
        binary: binary_name,
    };

    Ok(HttpResponse::Ok().json(response))
}

/// The `/` endpoint returns a simple status response.
async fn status() -> impl Responder {
    let response = StatusResponse {
        status: "ok".to_string(),
        version: "0.0.1".to_string(),
    };
    HttpResponse::Ok().json(response)
}

#[actix_web::main]
async fn main() -> std::io::Result<()> {
    println!("Starting Anchor Compiler Service on port 5000...");
    HttpServer::new(|| {
        App::new()
            .route("/", web::get().to(status))
            .route("/compile", web::post().to(compile))
            .route("/compile_json", web::post().to(compile_json))
    })
    .bind(("0.0.0.0", 5000))?
    .run()
    .await
}