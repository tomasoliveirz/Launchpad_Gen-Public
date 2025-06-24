from flask import Flask, request, jsonify
import subprocess
import os
import tempfile
import json
import logging

logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)

app = Flask(__name__)

@app.route("/api/slither/analyze", methods=["POST"])
def analyze():
    try:
        data = request.get_json()
        if not data:
            return jsonify({"error": "No JSON data provided"}), 400

        code = data.get("sourceCode")
        logger.info(f"Received source code: {len(code) if code else 0} characters")

        if not code:
            return jsonify({"error": "No source code provided"}), 400

        with tempfile.TemporaryDirectory() as tmpdir:
            file_path = os.path.join(tmpdir, "Contract.sol")

            with open(file_path, "w") as f:
                f.write(code)

            logger.info(f"Written source code to: {file_path}")

            output_file = os.path.join(tmpdir, "output.json")
            try:
                result = subprocess.run(
                    ["slither", file_path, "--json", output_file],
                    cwd=tmpdir,
                    capture_output=True,
                    text=True,
                    timeout=30
                )
                logger.info(f"Slither exit code: {result.returncode}")

                if os.path.exists(output_file):
                    with open(output_file, "r") as f:
                        output_content = f.read()
                    try:
                        json.loads(output_content)
                        return output_content, 200, {"Content-Type": "application/json"}
                    except json.JSONDecodeError as e:
                        logger.error(f"Invalid JSON output from Slither: {e}")
                        return jsonify({"error": "Slither produced invalid JSON output"}), 500

                complete_output = {
                    "success": False,
                    "exit_code": result.returncode,
                    "stdout": result.stdout.strip(),
                    "stderr": result.stderr.strip(),
                    "error": (result.stdout + result.stderr).strip()
                }
                
                error_msg = result.stderr.strip() or result.stdout.strip()
                if error_msg:
                    complete_output["error_summary"] = error_msg
                
                logger.error(f"Slither failed with exit code {result.returncode}")
                logger.error(f"STDOUT: {result.stdout}")
                logger.error(f"STDERR: {result.stderr}")
                
                return jsonify(complete_output), 400

            except subprocess.TimeoutExpired:
                logger.error("Slither analysis timed out")
                return jsonify({
                    "success": False,
                    "error": "Slither analysis timed out",
                    "timeout": True
                }), 500

            except FileNotFoundError:
                logger.error("Slither command not found")
                return jsonify({
                    "success": False,
                    "error": "Slither is not installed or not in PATH",
                    "installation_error": True
                }), 500

    except Exception as e:
        logger.error(f"Unexpected server error: {e}")
        return jsonify({
            "success": False,
            "error": f"Internal server error: {str(e)}",
            "server_error": True
        }), 500

@app.route("/api/health", methods=["GET"])
def health_check():
    try:
        result = subprocess.run(["slither", "--version"], capture_output=True, text=True, timeout=5)
        slither_version = result.stdout.strip() if result.returncode == 0 else "Not available"
        return jsonify({
            "status": "healthy",
            "slither_version": slither_version,
            "python_version": os.sys.version
        })
    except Exception as e:
        return jsonify({
            "status": "unhealthy",
            "error": str(e)
        }), 500

if __name__ == "__main__":
    logger.info("Starting Slither API server...")
    app.run(host="0.0.0.0", port=5000, debug=True)