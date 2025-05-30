using Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Processors;
using System.Text;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Generators
{
    public class SolidityCodeGenerator : ICodeGenerator<SolidityLanguageMetamodel>
    {
            public string Generate(SolidityLanguageMetamodel model)
            {
                try
                {
                    var codeBuilder = new StringBuilder();

                    var fileModel = CreateSolidityFileModel(model);
                    var fileHeaderCode = SolidityTemplateProcessor.FileHeader.Render(fileModel);
                    if (!string.IsNullOrWhiteSpace(fileHeaderCode))
                    {
                        codeBuilder.AppendLine(fileHeaderCode);
                        codeBuilder.AppendLine();
                    }

                    var importsCode = SolidityTemplateProcessor.Imports.Render(fileModel);
                    if (!string.IsNullOrWhiteSpace(importsCode))
                    {
                        codeBuilder.AppendLine(importsCode);
                        codeBuilder.AppendLine();
                    }

                    foreach (var contract in model.Contracts)
                    {
                        var contractCode = GenerateContract(contract);
                        codeBuilder.AppendLine(contractCode);
                    }

                    return codeBuilder.ToString();
                }
                catch (Exception ex)
                {
                    return $"// Error generating Solidity code: {ex.Message}";
                }
            }

            private SolidityFileModel CreateSolidityFileModel(SolidityLanguageMetamodel model)
            {
                return new SolidityFileModel
                {
                    FileHeader = model.FileHeader,
                    Contracts = model.Contracts
                };
            }

            private string GenerateContract(SolidityContractModel contract)
            {
                var contractBuilder = new StringBuilder();

                try
                {
                    var contractHeaderCode = SolidityTemplateProcessor.ContractHeader.Render(contract);
                    contractBuilder.AppendLine($"{contractHeaderCode} {{");

                    foreach (var enumModel in contract.Enums)
                    {
                        var enumCode = SolidityTemplateProcessor.Enums.Render(enumModel);
                        contractBuilder.AppendLine(AddIndentation(enumCode));
                        contractBuilder.AppendLine();
                    }

                    foreach (var structModel in contract.Structs)
                    {
                        var structCode = SolidityTemplateProcessor.Structs.Render(structModel);
                        contractBuilder.AppendLine(AddIndentation(structCode));
                        contractBuilder.AppendLine();
                    }

                    foreach (var errorModel in contract.Errors)
                    {
                        var errorCode = SolidityTemplateProcessor.Errors.Render(errorModel);
                        contractBuilder.AppendLine($"\t{errorCode}");
                    }

                    if (contract.Errors.Any())
                        contractBuilder.AppendLine();

                    foreach (var eventModel in contract.Events)
                    {
                        var eventCode = SolidityTemplateProcessor.Events.Render(eventModel);
                        contractBuilder.AppendLine($"\t{eventCode}");
                    }

                    if (contract.Events.Any())
                        contractBuilder.AppendLine();

                    foreach (var stateProperty in contract.StateProperties)
                    {
                        var stateCode = SolidityTemplateProcessor.StateProperties.Render(stateProperty);
                        contractBuilder.AppendLine($"\t{stateCode}");
                    }

                    if (contract.StateProperties.Any())
                        contractBuilder.AppendLine();

                    foreach (var modifier in contract.Modifiers)
                    {
                        var modifierCode = SolidityTemplateProcessor.Modifiers.Render(modifier);
                        contractBuilder.AppendLine(AddIndentation(modifierCode));
                        contractBuilder.AppendLine();
                    }

                    if (contract.ConstructorParameters.Any())
                    {
                        var constructorCode = SolidityTemplateProcessor.Constructor.Render(contract);
                        contractBuilder.AppendLine(AddIndentation(constructorCode));
                        contractBuilder.AppendLine();
                    }

                    foreach (var function in contract.Functions)
                    {
                        var functionCode = SolidityTemplateProcessor.Functions.Render(function);
                        contractBuilder.AppendLine(AddIndentation(functionCode));
                        contractBuilder.AppendLine();
                    }

                    contractBuilder.AppendLine("}");

                    return contractBuilder.ToString();
                }
                catch (Exception ex)
                {
                    return $"// Error generating contract {contract.Name}: {ex.Message}";
                }
            }

            private string AddIndentation(string code, int indentLevel = 1)
            {
                if (string.IsNullOrWhiteSpace(code))
                    return code;

                var indent = new string('\t', indentLevel);
                var lines = code.Split('\n');

                for (int i = 0; i < lines.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(lines[i]))
                    {
                        lines[i] = indent + lines[i];
                    }
                }

                return string.Join('\n', lines);
            }
        }
    }

