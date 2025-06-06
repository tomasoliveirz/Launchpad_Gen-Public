using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Generators;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Errors;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Events;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Header;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Modifiers;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Parameters;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.State;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.TypeReferences;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Version;

namespace CoreTests;

[TestClass]
public class SimpleContractExample
{
    [TestMethod]
    public void GenerateSimpleToken()
    {
        try
        {
            var generator = new SolidityCodeGenerator();

            var solidityModel = new SolidityLanguageMetamodel
            {
                FileHeader = new FileHeaderModel
                {
                    License = SpdxLicense.MIT,
                    Version = new VersionModel
                    {
                        Minimum = new SoftwareVersion { Major = 0, Minor = 8, Revision = 0 },
                        Maximum = new SoftwareVersion { Major = 0, Minor = 8, Revision = 26 }
                    }
                },

                Contracts = new[]
                {
                    new SolidityContractModel
                    {
                        Name = "SimpleToken",
                        ConstructorParameters = new List<ConstructorParameterModel>
                        {
                            new ConstructorParameterModel
                            {
                                Name = "initialSupply",
                                Type = new SimpleTypeReference(SolidityDataTypeEnum.Int8),
                            },
                            new ConstructorParameterModel
                            {
                                Name = "tokenName",
                                Type = new SimpleTypeReference(SolidityDataTypeEnum.String),
                            }
                        },
                        StateProperties = new List<StatePropertyModel>
                        {
                            new StatePropertyModel
                            {
                                Name = "totalSupply",
                                Type = new SimpleTypeReference(SolidityDataTypeEnum.Uint256),
                                Visibility = SolidityVisibilityEnum.Public,
                            },
                            new StatePropertyModel
                            {
                                Name = "name",
                                Type = new SimpleTypeReference(SolidityDataTypeEnum.String),
                                Visibility = SolidityVisibilityEnum.Public,
                            },
                            new StatePropertyModel
                            {
                                Name = "owner",
                                Type = new SimpleTypeReference(SolidityDataTypeEnum.Address),
                                Visibility = SolidityVisibilityEnum.Public,
                            },
                            new StatePropertyModel
                            {
                                Name = "balances",
                                Type = new MappingTypeReference(
                                    new SimpleTypeReference(SolidityDataTypeEnum.Address),
                                    new SimpleTypeReference(SolidityDataTypeEnum.Uint256)),
                                Visibility = SolidityVisibilityEnum.Private,
                            }
                        }
                    }
                }
            };

            string generatedCode = generator.Generate(solidityModel);

            Console.WriteLine("Generated Solidity Contract:");
            Console.WriteLine("============================");
            Console.WriteLine(generatedCode);
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ An error occurred during code generation:");
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            Assert.Fail("Test failed with exception: " + ex.Message);
        }
    }
}