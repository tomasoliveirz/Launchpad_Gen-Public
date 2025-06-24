from flask import Flask, request, jsonify
import subprocess
import os
import tempfile
from solcx import install_solc, set_solc_version

install_solc('0.8.19')
app = Flask(__name__)

@app.route("/api/slither/analyze", methods=["POST"])
def analyze():
    code = request.json.get("sourceCode")
    if not code:
        return jsonify({"error": "No source code provided"}), 400

    with tempfile.TemporaryDirectory() as tmpdir:
        file_path = os.path.join(tmpdir, "Contract.sol")
        with open(file_path, "w") as f:
            f.write(code)

        try:
            result = subprocess.run(
                ["slither", file_path, "--json", "output.json"],
                cwd=tmpdir,
                capture_output=True,
                text=True,
                timeout=30
            )

            output_path = os.path.join(tmpdir, "output.json")
            if os.path.exists(output_path):
                with open(output_path, "r") as f:
                    output = f.read()
                return output, 200, {"Content-Type": "application/json"}

            return jsonify({"error": result.stderr.strip()}), 400

        except subprocess.TimeoutExpired:
            return jsonify({"error": "Slither analysis timed out"}), 500



if __name__ == "__main__":
    app.run(host="0.0.0.0", port=5000)
