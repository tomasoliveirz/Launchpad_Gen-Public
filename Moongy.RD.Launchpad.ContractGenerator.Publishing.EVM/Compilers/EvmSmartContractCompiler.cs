using System.Diagnostics;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.EVM.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.EVM.Models;
using Newtonsoft.Json.Linq;

namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.EVM.Compilers
{
    public class EvmSmartContractCompiler : IEvmSmartContractCompiler
    {
        public EvmCompileResult Compile(string name, string sourceCode, SolidityVersion solidityVersion)
        {
            // Save the Solidity code temporarily
            var filePath = Path.Combine(Path.GetTempPath(), $"{name}.sol");
            File.WriteAllText(filePath, sourceCode);

            // Set up the compiler process
            var processInfo = new ProcessStartInfo
            {
                FileName = $"compilers/{solidityVersion.ToString}/solc",
                Arguments = $"--combined-json abi,bin {filePath}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            var process = new Process { StartInfo = processInfo };
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            if (!string.IsNullOrEmpty(error))
            {
                return new EvmCompileResult
                {
                    Success = false,
                    Errors = error
                };
            }

            var json = JObject.Parse(output);
            var contracts = json["contracts"];
            var contractKey = $"{name}.sol:{name}";

            return new EvmCompileResult
            {
                Success = true,
                Bytecode = contracts[contractKey]["bin"]!.ToString(),
                Abi = contracts[contractKey!]["abi"]!.ToString()
            };
        }

    }
}
