const express = require("express");
const bodyParser = require("body-parser");
const axios = require("axios");
const solc = require("solc");

const app = express();
app.use(bodyParser.json({ limit: "10mb" }));

async function getSolcVersions() {
    try {
        const res = await axios.get("https://solc-bin.ethereum.org/bin/list.json");
        return res.data; // e.g. "v0.8.20+commit.a1b79de6"
    } catch (error) {
        console.error("Error fetching latest version:", error);
    }
}

function getLatestRelease(list)
{
    return list.releases[list.latestRelease].replace("soljson-","").replace(".js", "");
}

app.get("/", async(req, res)=>{

    var versions = await getSolcVersions();
    res.json({status:"OK", version:"0.0.1", latestRelease:getLatestRelease(versions)})
})

app.post("/compile", async (req, res) => {
    const { code, version } = req.body;
    if (!code) {
        return res.status(400).json({ error: "Missing contract code" });
    }

    let compilerVersion = version;
    try {
        // If no version is provided, fetch the latest version
        if (!compilerVersion) {
            versions = await getSolcVersions();
            compilerVersion = getLatestRelease(versions);
        }

        // Load the specified compiler version
        solc.loadRemoteVersion(compilerVersion, (err, solcSnapshot) => {
            if (err) {
                console.error("Error loading compiler version:", err);
                return res.status(500).json({ error: "Failed to load compiler version" });
            }

            // Prepare the input JSON for the Solidity compiler
            const input = {
                language: "Solidity",
                sources: {
                    "Contract.sol": { content: code }
                },
                settings: {
                    outputSelection: {
                        "*": {
                            "*": ["abi", "evm.bytecode"]
                        }
                    }
                }
            };

            let output;
            try {
                output = JSON.parse(solcSnapshot.compile(JSON.stringify(input)));
            } catch (compileError) {
                console.error("Compilation error:", compileError);
                return res.status(500).json({ error: "Compilation error", details: compileError.toString() });
            }

            // Check for any compilation errors
            if (output.errors) {
                // Filter errors (ignoring warnings if needed)
                const errors = output.errors.filter(e => e.severity === 'error');
                if (errors.length > 0) {
                    return res.status(400).json({ error: "Compilation failed", errors });
                }
            }

            // Retrieve the first contract from the compiled output
            const contracts = output.contracts["Contract.sol"];
            const contractName = Object.keys(contracts)[0];
            const contractData = contracts[contractName];

            // Return the ABI, bytecode, and complete output
            return res.json({
                contractName,
                abi: contractData.abi,
                bytecode: contractData.evm.bytecode.object,
                rawOutput: output
            });
        });
    } catch (error) {
        console.error("Error in compile endpoint:", error);
        return res.status(500).json({ error: error.message });
    }
});

app.listen(3000, () => console.log("Solidity compiler running on port 3000"));