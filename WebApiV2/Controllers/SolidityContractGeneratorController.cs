using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Builders;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Exceptions;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Base;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Errors;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Events;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Functions;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Functions.Body;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Header;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Imports;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Modifiers;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Parameters;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.State;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.TypeReferences;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Version;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors;
using Moongy.RD.Launchpad.Core.Enums;

namespace Moongy.RD.Launchpad.ContractGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolidityContractGeneratorController : ControllerBase
    {
        [HttpGet("generate-erc20-token")]
        public ActionResult GenerateOpenZeppelinToken()
        {
            try
            {
                var contractCode = GenerateERC20TokenContractWithStructuredModel();
                return Ok(contractCode);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        private string GenerateERC20TokenContractWithStructuredModel()
        {
            var uint256Type = new SimpleTypeReference(SolidityDataTypeEnum.Uint256);
            var uint48Type = new SimpleTypeReference(SolidityDataTypeEnum.Uint48);
            var stringType = new SimpleTypeReference(SolidityDataTypeEnum.String);
            var boolType = new SimpleTypeReference(SolidityDataTypeEnum.Bool);
            var addressType = new SimpleTypeReference(SolidityDataTypeEnum.Address);
            var bytes4Type = new SimpleTypeReference(SolidityDataTypeEnum.Bytes4);

            FileHeaderModel fileHeader = new()
            {
                License = SpdxLicense.MIT,
                Version = new() 
                { 
                    Minimum = new() { Major = 0, Minor = 8, Revision = 27 }, 
                    Maximum = new() { Major = 0, Minor = 8, Revision = 27 } 
                }
            };

            var nameArgument = new ConstructorParameterModel()
            {
                Name = "name",
                Type = stringType,
                Value = "MyToken",
                Location = SolidityMemoryLocation.Memory
            };
            
            var symbolArgument = new ConstructorParameterModel()
            {
                Name = "symbol",
                Type = stringType,
                Value = "MTK",
                Location = SolidityMemoryLocation.Memory
            };

            var tokenBridgeArgument = new ConstructorParameterModel()
            {
                Name = "tokenBridge",
                Type = addressType,
                Index = 0,
                AssignedTo = "TOKEN_BRIDGE"
            };
            
            var recipientArgument = new ConstructorParameterModel()
            {
                Name = "recipient",
                Type = addressType,
                Index = 1
            };
            
            var initialOwnerArgument = new ConstructorParameterModel()
            {
                Name = "initialOwner",
                Type = addressType,
                Index = 2
            };

            var erc1363Import = new AbstractionImportModel()
            {
                Name = "ERC1363",
                PathName = "@openzeppelin/contracts/token/ERC20/extensions/ERC1363.sol",
                ConstructorParameters = []
            };

            var erc20Import = new AbstractionImportModel()
            {
                Name = "ERC20",
                PathName = "@openzeppelin/contracts/token/ERC20/ERC20.sol",
                ConstructorParameters = [nameArgument, symbolArgument]
            };

            var erc20BridgeableImport = new AbstractionImportModel()
            {
                Name = "ERC20Bridgeable",
                PathName = "@openzeppelin/community-contracts/token/ERC20/extensions/ERC20Bridgeable.sol",
                ConstructorParameters = []
            };

            var erc20BurnableImport = new AbstractionImportModel()
            {
                Name = "ERC20Burnable",
                PathName = "@openzeppelin/contracts/token/ERC20/extensions/ERC20Burnable.sol",
                ConstructorParameters = []
            };

            var erc20FlashMintImport = new AbstractionImportModel()
            {
                Name = "ERC20FlashMint",
                PathName = "@openzeppelin/contracts/token/ERC20/extensions/ERC20FlashMint.sol",
                ConstructorParameters = []
            };

            var erc20PausableImport = new AbstractionImportModel()
            {
                Name = "ERC20Pausable",
                PathName = "@openzeppelin/contracts/token/ERC20/extensions/ERC20Pausable.sol",
                ConstructorParameters = []
            };

            var erc20PermitImport = new AbstractionImportModel()
            {
                Name = "ERC20Permit",
                PathName = "@openzeppelin/contracts/token/ERC20/extensions/ERC20Permit.sol",
                ConstructorParameters = [nameArgument]
            };

            var erc20VotesImport = new AbstractionImportModel()
            {
                Name = "ERC20Votes",
                PathName = "@openzeppelin/contracts/token/ERC20/extensions/ERC20Votes.sol",
                ConstructorParameters = []
            };

            var ownableImport = new AbstractionImportModel()
            {
                Name = "Ownable",
                PathName = "@openzeppelin/contracts/access/Ownable.sol",
                ConstructorParameters = [initialOwnerArgument]
            };

            var noncesImport = new ImportModel()
            {
                PathName = "@openzeppelin/contracts/utils/Nonces.sol",
                Alias = null
            };

            var tokenBridgeProperty = new StatePropertyModel()
            {
                Name = "TOKEN_BRIDGE",
                Type = addressType,
                Visibility = SolidityVisibilityEnum.Public,
                IsImmutable = true
            };

            var unauthorizedError = new ErrorModel()
            {
                Name = "Unauthorized",
                Parameters = []
            };

            var functions = new List<FunctionModel>();
            
            var checkTokenBridgeFunction = new FunctionModel()
            {
                Name = "_checkTokenBridge",
                FunctionType = SolidityFunctionTypeEnum.Normal,
                Parameters = [new FunctionParameterModel { 
                    Name = "caller", 
                    Type = addressType, 
                    Index = 0 
                }],
                Visibility = SolidityVisibilityEnum.Internal,
                Mutability = SolidityFunctionMutabilityEnum.View,
                IsOverride = true,
                OverrideSpecifiers = new List<string>(),
                Body = FunctionBodyModel.Create(body => {
                    var ifStatement = body.AddIfStatement("caller != TOKEN_BRIDGE");
                    ifStatement.AddThenStatement(new StatementInfo { 
                        Type = StatementType.Revert,
                        Name = "Unauthorized" 
                    });
                })
            };
            functions.Add(checkTokenBridgeFunction);
            
            var pauseFunction = new FunctionModel()
            {
                Name = "pause",
                FunctionType = SolidityFunctionTypeEnum.Normal,
                Parameters = [],
                Visibility = SolidityVisibilityEnum.Public,
                Modifiers = [new ModifierModel { 
                    Name = "onlyOwner", 
                    Body = "require(msg.sender == owner(), \"Not the owner\");" 
                }],
                Body = FunctionBodyModel.Create(body => {
                    body.AddMethodCall("_pause");
                })
            };
            functions.Add(pauseFunction);
            
            var unpauseFunction = new FunctionModel()
            {
                Name = "unpause",
                FunctionType = SolidityFunctionTypeEnum.Normal,
                Parameters = [],
                Visibility = SolidityVisibilityEnum.Public,
                Modifiers = [new ModifierModel { 
                    Name = "onlyOwner", 
                    Body = "require(msg.sender == owner(), \"Not the owner\");" 
                }],
                Body = FunctionBodyModel.Create(body => {
                    body.AddMethodCall("_unpause");
                })
            };
            functions.Add(unpauseFunction);
            
            var clockFunction = new FunctionModel()
            {
                Name = "clock",
                FunctionType = SolidityFunctionTypeEnum.Normal,
                Parameters = [],
                ReturnParameters = [new ReturnParameterModel { 
                    Name = "", 
                    Type = uint48Type,
                    Index = 0
                }],
                Visibility = SolidityVisibilityEnum.Public,
                Mutability = SolidityFunctionMutabilityEnum.View,
                IsOverride = true,
                OverrideSpecifiers = new List<string>(),
                Body = FunctionBodyModel.Create(body => {
                    body.AddReturnStatement("uint48(block.timestamp)");
                })
            };
            functions.Add(clockFunction);
            
            var clockModeFunction = new FunctionModel()
            {
                Name = "CLOCK_MODE",
                FunctionType = SolidityFunctionTypeEnum.Normal,
                Parameters = [],
                ReturnParameters = [new ReturnParameterModel { 
                    Name = "", 
                    Type = stringType, 
                    Location = SolidityMemoryLocation.Memory,
                    Index = 0
                }],
                Visibility = SolidityVisibilityEnum.Public,
                Mutability = SolidityFunctionMutabilityEnum.Pure,
                IsOverride = true,
                OverrideSpecifiers = new List<string>(),
                Body = FunctionBodyModel.Create(body => {
                    body.AddReturnStatement("\"mode=timestamp\"");
                })
            };
            functions.Add(clockModeFunction);
            
            var updateFunction = new FunctionModel()
            {
                Name = "_update",
                FunctionType = SolidityFunctionTypeEnum.Normal,
                Parameters = [
                    new FunctionParameterModel { Name = "from", Type = addressType, Index = 0 },
                    new FunctionParameterModel { Name = "to", Type = addressType, Index = 1 },
                    new FunctionParameterModel { Name = "value", Type = uint256Type, Index = 2 }
                ],
                Visibility = SolidityVisibilityEnum.Internal,
                IsOverride = true,
                OverrideSpecifiers = new List<string> { "ERC20", "ERC20Pausable", "ERC20Votes" },
                Body = FunctionBodyModel.Create(body => {
                    body.AddMethodCall("super._update", "from", "to", "value");
                })
            };
            functions.Add(updateFunction);
            
            var supportsInterfaceFunction = new FunctionModel()
            {
                Name = "supportsInterface",
                FunctionType = SolidityFunctionTypeEnum.Normal,
                Parameters = [
                    new FunctionParameterModel { Name = "interfaceId", Type = bytes4Type, Index = 0 }
                ],
                ReturnParameters = [
                    new ReturnParameterModel { Name = "", Type = boolType, Index = 0 }
                ],
                Visibility = SolidityVisibilityEnum.Public,
                Mutability = SolidityFunctionMutabilityEnum.View,
                IsOverride = true,
                OverrideSpecifiers = new List<string> { "ERC20Bridgeable", "ERC1363" },
                Body = FunctionBodyModel.Create(body => {
                    body.AddReturnStatement("super.supportsInterface(interfaceId)");
                })
            };
            functions.Add(supportsInterfaceFunction);
            
            var noncesFunction = new FunctionModel()
            {
                Name = "nonces",
                FunctionType = SolidityFunctionTypeEnum.Normal,
                Parameters = [
                    new FunctionParameterModel { Name = "owner", Type = addressType, Index = 0 }
                ],
                ReturnParameters = [
                    new ReturnParameterModel { Name = "", Type = uint256Type, Index = 0 }
                ],
                Visibility = SolidityVisibilityEnum.Public,
                Mutability = SolidityFunctionMutabilityEnum.View,
                IsOverride = true,
                OverrideSpecifiers = new List<string> { "ERC20Permit", "Nonces" },
                Body = FunctionBodyModel.Create(body => {
                    body.AddReturnStatement("super.nonces(owner)");
                })
            };
            functions.Add(noncesFunction);

            SolidityContractModel contract = new()
            {
                Name = "MyToken",
                BaseContracts = [
                    erc20Import,
                    erc20BridgeableImport,
                    erc20BurnableImport,
                    erc20PausableImport,
                    ownableImport,
                    erc1363Import,
                    erc20PermitImport,
                    erc20VotesImport,
                    erc20FlashMintImport
                ],
                Imports = [noncesImport],
                ConstructorParameters = [tokenBridgeArgument, recipientArgument, initialOwnerArgument],
                StateProperties = [tokenBridgeProperty],
                Errors = [unauthorizedError],
                Functions = functions
            };
            
            SolidityFile file = new()
            {
                FileHeader = fileHeader,
                Contracts = [contract]
            };

            StringBuilder resultBuilder = new StringBuilder();
            
            resultBuilder.AppendLine("// SPDX-License-Identifier: MIT");
            resultBuilder.AppendLine("// Compatible with OpenZeppelin Contracts ^5.0.0");
            resultBuilder.AppendLine("pragma solidity ^0.8.27;");
            resultBuilder.AppendLine();
            resultBuilder.AppendLine(SolidityTemplateProcessor.Imports.Render(file));
            resultBuilder.AppendLine();
            resultBuilder.AppendLine(SolidityTemplateProcessor.ContractHeader.Render(contract));
            resultBuilder.AppendLine();
            
            foreach (var stateProperty in contract.StateProperties)
            {
                resultBuilder.AppendLine(SolidityTemplateProcessor.StateProperties.Render(stateProperty));
            }
            resultBuilder.AppendLine();
            
            foreach (var error in contract.Errors)
            {
                resultBuilder.AppendLine(SolidityTemplateProcessor.Errors.Render(error));
            }
            resultBuilder.AppendLine();
            
            resultBuilder.AppendLine(SolidityTemplateProcessor.Constructor.Render(contract));
            resultBuilder.AppendLine();
            
            foreach (var function in contract.Functions)
            {
                resultBuilder.AppendLine(SolidityTemplateProcessor.Functions.Render(function));
                resultBuilder.AppendLine();
            }
            
            resultBuilder.AppendLine("}");

            return resultBuilder.ToString();
        }

        [HttpGet("generate-simple-erc20")]
        public ActionResult GenerateSimpleERC20Token()
        {
            try
            {
                var uint256Type = new SimpleTypeReference(SolidityDataTypeEnum.Uint256);
                var stringType = new SimpleTypeReference(SolidityDataTypeEnum.String);
                var addressType = new SimpleTypeReference(SolidityDataTypeEnum.Address);
                var boolType = new SimpleTypeReference(SolidityDataTypeEnum.Bool);

                var fileHeader = new FileHeaderModel
                {
                    License = SpdxLicense.MIT,
                    Version = new VersionModel
                    {
                        Minimum = new SoftwareVersion { Major = 0, Minor = 8, Revision = 20 },
                        Maximum = new SoftwareVersion { Major = 0, Minor = 8, Revision = 20 }
                    }
                };

                var nameArg = new ConstructorParameterModel 
                { 
                    Name = "name", 
                    Type = stringType, 
                    Value = "SimpleToken",
                    Location = SolidityMemoryLocation.Memory
                };
                
                var symbolArg = new ConstructorParameterModel 
                { 
                    Name = "symbol", 
                    Type = stringType, 
                    Value = "SIM",
                    Location = SolidityMemoryLocation.Memory
                };
                
                var initialOwnerArg = new ConstructorParameterModel 
                { 
                    Name = "initialOwner", 
                    Type = addressType
                };

                var erc20Import = new AbstractionImportModel
                {
                    Name = "ERC20",
                    PathName = "@openzeppelin/contracts/token/ERC20/ERC20.sol",
                    ConstructorParameters = [nameArg, symbolArg]
                };
                
                var ownableImport = new AbstractionImportModel
                {
                    Name = "Ownable",
                    PathName = "@openzeppelin/contracts/access/Ownable.sol",
                    ConstructorParameters = [initialOwnerArg]
                };

                var toParam = new FunctionParameterModel
                {
                    Name = "to",
                    Type = addressType,
                    Index = 0
                };
                
                var amountParam = new FunctionParameterModel
                {
                    Name = "amount",
                    Type = uint256Type,
                    Index = 1
                };

                var mintFunction = new FunctionModel
                {
                    Name = "mint",
                    FunctionType = SolidityFunctionTypeEnum.Normal,
                    Parameters = [toParam, amountParam],
                    Visibility = SolidityVisibilityEnum.Public,
                    Modifiers = [new ModifierModel { 
                        Name = "onlyOwner", 
                        Body = "require(msg.sender == owner());" 
                    }],
                    Body = FunctionBodyModel.Create(body => {
                        body.AddRequireStatement("to != address(0)", "Cannot mint to zero address");
                        body.AddMethodCall("_mint", "to", "amount");
                    })
                };

                var contract = new SolidityContractModel
                {
                    Name = "SimpleToken",
                    BaseContracts = [erc20Import, ownableImport],
                    ConstructorParameters = [nameArg, symbolArg, initialOwnerArg],
                    Functions = [mintFunction]
                };

                var file = new SolidityFile
                {
                    FileHeader = fileHeader,
                    Contracts = [contract]
                };

                StringBuilder resultBuilder = new StringBuilder();
                
                resultBuilder.AppendLine(SolidityTemplateProcessor.FileHeader.Render(file));
                resultBuilder.AppendLine();
                resultBuilder.AppendLine(SolidityTemplateProcessor.Imports.Render(file));
                resultBuilder.AppendLine();
                resultBuilder.AppendLine(SolidityTemplateProcessor.ContractHeader.Render(contract));
                resultBuilder.AppendLine();
                resultBuilder.AppendLine(SolidityTemplateProcessor.Constructor.Render(contract));
                resultBuilder.AppendLine();
                
                foreach (var function in contract.Functions)
                {
                    resultBuilder.AppendLine(SolidityTemplateProcessor.Functions.Render(function));
                    resultBuilder.AppendLine();
                }
                
                resultBuilder.AppendLine("}");

                return Ok(resultBuilder.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest($"Error generating contract: {ex.Message}");
            }
        }

        private FunctionBodyModel CreateConstructorBody(
            string tokenBridgeParamName, 
            string recipientParamName, 
            uint initialAmount = 301)
        {
            return FunctionBodyModel.Create(body => {
                body.AddRequireStatement($"{tokenBridgeParamName} != address(0)", "Invalid TOKEN_BRIDGE address");
                body.AddAssignment("TOKEN_BRIDGE", tokenBridgeParamName);
                var ifStatement = body.AddIfStatement("block.chainid == 140");
                ifStatement.AddThenStatement(new StatementInfo 
                { 
                    Type = StatementType.MethodCall,
                    Name = "_mint", 
                    Arguments = new List<string> { recipientParamName, $"{initialAmount} * (10 ** decimals())" } 
                });
            });
        }
    }
}