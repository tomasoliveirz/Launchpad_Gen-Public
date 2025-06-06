using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Generators;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Errors;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Events;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Header;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Imports;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Modifiers;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Parameters;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.State;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Statements;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Structs;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.TypeReferences;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Version;

namespace CoreTests
{
    [TestClass]
    public class TestingTemplatesValidContract
    {
        [TestMethod]
        public void TestValidContractGeneration()
        {
            var generator = new SolidityCodeGenerator();

            #region FileHeader
            var fileHeader = new FileHeaderModel
            {
                License = SpdxLicense.MIT,
                Version = new VersionModel
                {
                    Minimum = new SoftwareVersion { Major = 0, Minor = 8, Revision = 0 },
                    Maximum = new SoftwareVersion { Major = 0, Minor = 8, Revision = 26 }
                }
            };
            #endregion

            #region ConstructorParameters
            var constructParams = new List<ConstructorParameterModel>
{
    new ConstructorParameterModel
    {
        Name = "initialSupply",
        Type = new SimpleTypeReference(SolidityDataTypeEnum.Uint256), // Fixed: Changed from Int8 to Uint256
    },
    new ConstructorParameterModel
    {
        Name = "tokenName",
        Type = new SimpleTypeReference(SolidityDataTypeEnum.String),
    }
};
            #endregion

            #region StateProperties
            var stateProperties = new List<StatePropertyModel>
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
};
            #endregion

            #region NormalImports 
            var imports = new List<ImportModel>
{
    new ImportModel
    {
        Code = null,
        PathName = "openzeppelin-solidity/contracts/token/ERC20/IERC20.sol",
        Alias = "IERC20"
    },
    new ImportModel
    {
        Code = null,
        PathName = "openzeppelin-solidity/contracts/token/ERC20/ERC20.sol",
        Alias = "ERC20"
    }
};
            #endregion

            #region AbstractionImports
            var baseContracts = new List<AbstractionImportModel>
{
    new AbstractionImportModel
    {
        Name = "IERC20",
        Code = null,
        PathName = "openzeppelin-solidity/contracts/token/ERC20/IERC20.sol",
        ConstructorParameters = constructParams,
        Alias = "IERC20Interface"
    }
};
            #endregion

            #region InterfacesImports
            var interfaces = new List<InterfaceImportModel>
{
    new InterfaceImportModel
    {
        Name = "IERC20",
        Code = null,
        PathName = "openzeppelin-solidity/contracts/token/ERC20/IERC20.sol",
        Alias = "IERC20Interface"
    }
};
            #endregion

            #region TypeUtilitiesImports
            var typeUtilities = new List<TypeUtilityImportModel>
{
    new TypeUtilityImportModel
    {
        Name = "SafeMath",
        Type = new SimpleTypeReference(SolidityDataTypeEnum.String),
        Code = null,
        Alias = "SafeMathLibrary",
        PathName = "openzeppelin-solidity/contracts/math/SafeMath.sol"
    }
};
            #endregion

            #region Enums
            var enums = new List<EnumModel>
{
    new EnumModel
    {
        Name = "TokenType",
        Values = new List<string> { "ERC20", "ERC721", "ERC1155" }
    }
};
            #endregion

            #region Errors
            var errors = new List<ErrorModel>
{
    new ErrorModel
    {
        Name = "InsufficientBalance",
        Parameters = new List<ErrorParameterModel>
        {
            new ErrorParameterModel
            {
                Name = "requested",
                Type = new SimpleTypeReference(SolidityDataTypeEnum.Uint256)
            },
            new ErrorParameterModel
            {
                Name = "available",
                Type = new SimpleTypeReference(SolidityDataTypeEnum.Uint256)
            }
        },
    },
    new ErrorModel
    {
        Name = "Unauthorized",
        Parameters = new List<ErrorParameterModel>
        {
            new ErrorParameterModel
            {
                Name = "caller",
                Type = new SimpleTypeReference(SolidityDataTypeEnum.Address)
            }
        },
    }
};
            #endregion

            #region Structs
            var structs = new List<StructModel>
            {
        new() {
            Name = "TokenHolder",
            Properties = new StructPropertyModel[]
            {
                new()
                {
                    Name = "holderAddress",
                    DataType = new SimpleTypeReference(SolidityDataTypeEnum.Address)
                },
                new()
                {
                    Name = "balance",
                    DataType = new SimpleTypeReference(SolidityDataTypeEnum.Uint256)
                }
            } 
        }
    };
                #endregion

            #region Modifiers
            var modifiers = new List<ModifierModel>
{
    new ModifierModel
    {
        Name = "onlyOwner",
        Parameters = new List<ModifierParameterModel>(), // Fixed: Empty parameters for this modifier
        Arguments = new List<string>(),
        Body = "require(msg.sender == owner, \"Caller is not the owner\");"
    },
    new ModifierModel
    {
        Name = "hasSufficientBalance",
        Parameters = new List<ModifierParameterModel>
        {
            new ModifierParameterModel
            {
                Name = "amount",
                Type = new SimpleTypeReference(SolidityDataTypeEnum.Uint256)
            }
        },
        Arguments = new List<string>(),
        Body = "require(balances[msg.sender] >= amount, \"Insufficient balance\");"
    }
};
            #endregion

            #region Events
            var events = new List<EventModel>
{
    new EventModel
    {
        Name = "Transfer",
        Parameters = new List<EventParameterModel>
        {
            new EventParameterModel
            {
                Name = "from",
                Type = new SimpleTypeReference(SolidityDataTypeEnum.Address),
                IsIndexed = true
            },
            new EventParameterModel
            {
                Name = "to",
                Type = new SimpleTypeReference(SolidityDataTypeEnum.Address),
                IsIndexed = true
            },
            new EventParameterModel
            {
                Name = "value",
                Type = new SimpleTypeReference(SolidityDataTypeEnum.Uint256),
                IsIndexed = false
            }
        }
    }
};
            #endregion



            #region Functions
            var functions = new List<BaseFunctionModel>
{
    
    // Transfer function
    new NormalFunctionModel
    {
        Name = "transfer",
        Parameters = new List<FunctionParameterModel>
        {
            new FunctionParameterModel
            {
                Name = "to",
                Type = new SimpleTypeReference(SolidityDataTypeEnum.Address)
            },
            new FunctionParameterModel
            {
                Name = "amount",
                Type = new SimpleTypeReference(SolidityDataTypeEnum.Uint256)
            }
        },
        ReturnParameters = new List<ReturnParameterModel>
        {
            new ReturnParameterModel
            {
                Name = "success",
                Type = new SimpleTypeReference(SolidityDataTypeEnum.Bool)
            }
        },
        Visibility = SolidityVisibilityEnum.Public,
        Mutability = SolidityFunctionMutabilityEnum.Payable,
        Modifiers = new List<ModifierModel> { modifiers[1] },
        Statements = new List<StatementModel>
        {
            new RequireStatement
            {
                Condition = new LiteralExpressionModel("to != address(0)"),
                Message = "Cannot transfer to zero address"
            },
            new AssignmentStatement
            (
                new IdentifierExpressionModel("balances[msg.sender]"),
                new BinaryExpressionModel(
                    new IdentifierExpressionModel("balances[msg.sender]"),
                    OperatorEnum.Subtract,
                    new IdentifierExpressionModel("amount") 
                )
            ),
            new AssignmentStatement
            (
                new IdentifierExpressionModel("balances[to]"),
                new BinaryExpressionModel(
                    new IdentifierExpressionModel("balances[to]"),
                    OperatorEnum.Add,
                    new IdentifierExpressionModel("amount")
                )
            ),

            new EmitStatement("Transfer")
                .AddStringArgument("msg.sender")
                .AddStringArgument("to")
                .AddStringArgument("amount"),
            new ReturnStatement
            {
                ValueExpressions = new List<ExpressionModel>
                {
                    new LiteralExpressionModel("true")
                }
            }
        }
    },
    
    new NormalFunctionModel
    {
        Name = "balanceOf",
        Parameters = new List<FunctionParameterModel>
        {
            new FunctionParameterModel
            {
                Name = "account",
                Type = new SimpleTypeReference(SolidityDataTypeEnum.Address)
            }
        },
        ReturnParameters = new List<ReturnParameterModel>
        {
            new ReturnParameterModel
            {
                Name = "balance",
                Type = new SimpleTypeReference(SolidityDataTypeEnum.Uint256)
            }
        },
        Visibility = SolidityVisibilityEnum.Public,
        Mutability = SolidityFunctionMutabilityEnum.View,
        Statements = new List<StatementModel>
        {
            new ReturnStatement
            {
                ValueExpressions = new List<ExpressionModel>
                {
                    new IdentifierExpressionModel("balances[account]")
                }
            }
        }
    }
};
            #endregion

            var contractModel = new SolidityLanguageMetamodel
            {
                FileHeader = fileHeader,
                Contracts = new[]
                {
                    new SolidityContractModel
                    {
                Name = "MyToken",
                Imports = imports,
                BaseContracts = baseContracts,
                Interfaces = interfaces,
                TypeUtilities = typeUtilities,
                Enums = enums,
                Errors = errors,
                Structs = structs,
                StateProperties = stateProperties,
                Modifiers = modifiers,
                Events = events,
                Functions = functions,
                ConstructorParameters = constructParams,
                }
               }
            };

            string generatedContract = generator.Generate(contractModel);
            Console.WriteLine("Generated Solidity Contract:");
            Console.WriteLine("============================");
            Console.WriteLine(generatedContract);
        }
    }
}
