from flask import Flask, request, jsonify
import subprocess
import os
import tempfile
import json
import logging
from solcx import install_solc, set_solc_version

logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)

try:
    install_solc('0.8.19')
    set_solc_version('0.8.19')
    logger.info("Solidity compiler installed successfully")
except Exception as e:
    logger.error(f"Failed to install Solidity compiler: {e}")

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

            try:
                output_file = os.path.join(tmpdir, "output.json")
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
                    
                    # Validate JSON
                    try:
                        json.loads(output_content)
                        return output_content, 200, {"Content-Type": "application/json"}
                    except json.JSONDecodeError as e:
                        logger.error(f"Invalid JSON output: {e}")
                        return jsonify({"error": "Slither produced invalid JSON output"}), 500
                
                #  no output file, check for errors
                error_msg = result.stderr.strip() if result.stderr else result.stdout.strip()
                logger.error(f"Slither error: {error_msg}")
                
                # Try to extract meaningful error message
                if "Error:" in error_msg:
                    error_lines = [line for line in error_msg.split('\n') if 'Error:' in line]
                    if error_lines:
                        return jsonify({"error": error_lines[-1]}), 400
                
                return jsonify({
                    "error": error_msg or "Slither analysis failed without output",
                    "stdout": result.stdout,
                    "stderr": result.stderr
                }), 400

            except subprocess.TimeoutExpired:
                logger.error("Slither analysis timed out")
                return jsonify({"error": "Slither analysis timed out"}), 500
            
            except FileNotFoundError:
                logger.error("Slither command not found")
                return jsonify({"error": "Slither is not installed or not in PATH"}), 500

    except Exception as e:
        logger.error(f"Unexpected error: {e}")
        return jsonify({"error": f"Internal server error: {str(e)}"}), 500

@app.route("/api/health", methods=["GET"])
def health_check():
    """Health check endpoint"""
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