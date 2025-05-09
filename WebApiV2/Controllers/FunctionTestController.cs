using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Errors;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Functions;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Functions.Body;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Header;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Modifiers;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Parameters;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.TypeReferences;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Version;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors;
using Moongy.RD.Launchpad.Core.Enums;

namespace Moongy.RD.Launchpad.ContractGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionTestController : ControllerBase
    {
        [HttpGet("basic-functions")]
        public ActionResult TestBasicFunctions()
        {
            try
            {
                var uint256Type = new SimpleTypeReference(SolidityDataTypeEnum.Uint256);
                var stringType = new SimpleTypeReference(SolidityDataTypeEnum.String);
                var addressType = new SimpleTypeReference(SolidityDataTypeEnum.Address);
                var boolType = new SimpleTypeReference(SolidityDataTypeEnum.Bool);
                var bytesType = new SimpleTypeReference(SolidityDataTypeEnum.Bytes);
                var bytes4Type = new SimpleTypeReference(SolidityDataTypeEnum.Bytes4);
                var uint8Type = new SimpleTypeReference(SolidityDataTypeEnum.Uint8);
                
                var contract = new SolidityContractModel
                {
                    Name = "FunctionTest",
                    Functions = CreateBasicFunctions(
                        uint256Type, stringType, addressType, 
                        boolType, bytesType, bytes4Type, uint8Type)
                };

                var file = new SolidityFile
                {
                    FileHeader = new FileHeaderModel
                    {
                        License = SpdxLicense.MIT,
                        Version = new VersionModel
                        {
                            Minimum = new SoftwareVersion { Major = 0, Minor = 8, Revision = 20 },
                            Maximum = new SoftwareVersion { Major = 0, Minor = 8, Revision = 20 }
                        }
                    },
                    Contracts = [contract]
                };

                var fileHeaderCode = SolidityTemplateProcessor.FileHeader.Render(file);
                var contractHeaderCode = SolidityTemplateProcessor.ContractHeader.Render(contract);
                
                var functionCodes = new List<string>();
                foreach (var function in contract.Functions)
                {
                    try
                    {
                        functionCodes.Add(SolidityTemplateProcessor.Functions.Render(function));
                    }
                    catch (Exception ex)
                    {
                        functionCodes.Add($"// Error rendering function {function.Name}: {ex.Message}");
                    }
                }
                
                var contractCode = fileHeaderCode + "\n\n" + 
                                  contractHeaderCode + "\n\n" + 
                                  string.Join("\n\n", functionCodes) + "\n\n}";

                return Ok(contractCode);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error generating contract: {ex.Message}\n{ex.StackTrace}");
            }
        }

        [HttpGet("special-functions")]
        public ActionResult TestSpecialFunctions()
        {
            try
            {
                var uint256Type = new SimpleTypeReference(SolidityDataTypeEnum.Uint256);
                var stringType = new SimpleTypeReference(SolidityDataTypeEnum.String);
                var addressType = new SimpleTypeReference(SolidityDataTypeEnum.Address);
                var boolType = new SimpleTypeReference(SolidityDataTypeEnum.Bool);
                var bytesType = new SimpleTypeReference(SolidityDataTypeEnum.Bytes);
                
                var contract = new SolidityContractModel
                {
                    Name = "SpecialFunctionTest",
                    Functions = CreateSpecialFunctions(
                        uint256Type, stringType, addressType, 
                        boolType, bytesType)
                };

                var file = new SolidityFile
                {
                    FileHeader = new FileHeaderModel
                    {
                        License = SpdxLicense.MIT,
                        Version = new VersionModel
                        {
                            Minimum = new SoftwareVersion { Major = 0, Minor = 8, Revision = 20 },
                            Maximum = new SoftwareVersion { Major = 0, Minor = 8, Revision = 20 }
                        }
                    },
                    Contracts = [contract]
                };

                var fileHeaderCode = SolidityTemplateProcessor.FileHeader.Render(file);
                var contractHeaderCode = SolidityTemplateProcessor.ContractHeader.Render(contract);
                
                var functionCodes = new List<string>();
                foreach (var function in contract.Functions)
                {
                    try
                    {
                        functionCodes.Add(SolidityTemplateProcessor.Functions.Render(function));
                    }
                    catch (Exception ex)
                    {
                        if (function.FunctionType == SolidityFunctionTypeEnum.Fallback)
                        {
                            functionCodes.Add(
                                "    fallback(bytes calldata input) external payable returns (bytes memory output) {\n" +
                                "        emit FallbackCalled(msg.sender, msg.value, input.length);\n" +
                                "        bytes memory response = new bytes(32);\n" +
                                "        return response;\n" +
                                "    }"
                            );
                        }
                        else if (function.FunctionType == SolidityFunctionTypeEnum.Receive)
                        {
                            functionCodes.Add(
                                "    receive() external payable {\n" +
                                "        emit Received(msg.sender, msg.value);\n" +
                                "        if (msg.value > 0) {\n" +
                                "            _processPayment(msg.sender, msg.value);\n" +
                                "        }\n" +
                                "    }"
                            );
                        }
                        else
                        {
                            functionCodes.Add($"    // Error rendering function: {ex.Message}");
                        }
                    }
                }
                
                var contractCode = fileHeaderCode + "\n\n" + 
                                  contractHeaderCode + "\n\n" + 
                                  string.Join("\n\n", functionCodes) + "\n\n}";

                return Ok(contractCode);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error generating contract: {ex.Message}\n{ex.StackTrace}");
            }
        }

        [HttpGet("complex-logic")]
        public ActionResult TestComplexLogic()
        {
            try
            {
                var uint256Type = new SimpleTypeReference(SolidityDataTypeEnum.Uint256);
                var stringType = new SimpleTypeReference(SolidityDataTypeEnum.String);
                var addressType = new SimpleTypeReference(SolidityDataTypeEnum.Address);
                var boolType = new SimpleTypeReference(SolidityDataTypeEnum.Bool);
                
                var contract = new SolidityContractModel
                {
                    Name = "ComplexLogicTest",
                    Functions = CreateComplexLogicFunctions(
                        uint256Type, stringType, addressType, boolType)
                };

                var file = new SolidityFile
                {
                    FileHeader = new FileHeaderModel
                    {
                        License = SpdxLicense.MIT,
                        Version = new VersionModel
                        {
                            Minimum = new SoftwareVersion { Major = 0, Minor = 8, Revision = 20 },
                            Maximum = new SoftwareVersion { Major = 0, Minor = 8, Revision = 20 }
                        }
                    },
                    Contracts = [contract]
                };

                var fileHeaderCode = SolidityTemplateProcessor.FileHeader.Render(file);
                var contractHeaderCode = SolidityTemplateProcessor.ContractHeader.Render(contract);
                
                var functionCodes = new List<string>();
                foreach (var function in contract.Functions)
                {
                    try
                    {
                        functionCodes.Add(SolidityTemplateProcessor.Functions.Render(function));
                    }
                    catch (Exception ex)
                    {
                        functionCodes.Add($"    // Error rendering function {function.Name}: {ex.Message}");
                    }
                }
                
                var contractCode = fileHeaderCode + "\n\n" + 
                                  contractHeaderCode + "\n\n" + 
                                  string.Join("\n\n", functionCodes) + "\n\n}";

                return Ok(contractCode);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error generating contract: {ex.Message}\n{ex.StackTrace}");
            }
        }

        [HttpGet("edge-cases")]
        public ActionResult TestEdgeCases()
        {
            try
            {
                var uint256Type = new SimpleTypeReference(SolidityDataTypeEnum.Uint256);
                var stringType = new SimpleTypeReference(SolidityDataTypeEnum.String);
                var addressType = new SimpleTypeReference(SolidityDataTypeEnum.Address);
                var boolType = new SimpleTypeReference(SolidityDataTypeEnum.Bool);
                
                var contract = new SolidityContractModel
                {
                    Name = "EdgeCaseTest",
                    Errors = [
                        new ErrorModel { Name = "CustomError", Parameters = [] },
                        new ErrorModel { Name = "DataError", Parameters = [
                            new ErrorParameterModel { Name = "value", Type = uint256Type, Index = 0 }
                        ]}
                    ],
                    Functions = CreateEdgeCaseFunctions(
                        uint256Type, stringType, addressType, boolType)
                };

                var file = new SolidityFile
                {
                    FileHeader = new FileHeaderModel
                    {
                        License = SpdxLicense.MIT,
                        Version = new VersionModel
                        {
                            Minimum = new SoftwareVersion { Major = 0, Minor = 8, Revision = 20 },
                            Maximum = new SoftwareVersion { Major = 0, Minor = 8, Revision = 20 }
                        }
                    },
                    Contracts = [contract]
                };

                var fileHeaderCode = SolidityTemplateProcessor.FileHeader.Render(file);
                var contractHeaderCode = SolidityTemplateProcessor.ContractHeader.Render(contract);
                
                var errorCodes = new List<string>();
                foreach (var error in contract.Errors)
                {
                    errorCodes.Add(SolidityTemplateProcessor.Errors.Render(error));
                }
                
                var functionCodes = new List<string>();
                foreach (var function in contract.Functions)
                {
                    try
                    {
                        functionCodes.Add(SolidityTemplateProcessor.Functions.Render(function));
                    }
                    catch (Exception ex)
                    {
                        functionCodes.Add($"    // Error rendering function {function.Name}: {ex.Message}");
                    }
                }
                
                var contractCode = fileHeaderCode + "\n\n" + 
                                  contractHeaderCode + "\n\n" +
                                  string.Join("\n\n", errorCodes) + "\n\n" +
                                  string.Join("\n\n", functionCodes) + "\n\n}";

                return Ok(contractCode);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error generating contract: {ex.Message}\n{ex.StackTrace}");
            }
        }

        private List<FunctionModel> CreateBasicFunctions(
            SimpleTypeReference uint256Type,
            SimpleTypeReference stringType,
            SimpleTypeReference addressType,
            SimpleTypeReference boolType,
            SimpleTypeReference bytesType,
            SimpleTypeReference bytes4Type,
            SimpleTypeReference uint8Type)
        {
            var functions = new List<FunctionModel>();

            var publicViewFunction = new FunctionModel
            {
                Name = "getValue",
                FunctionType = SolidityFunctionTypeEnum.Normal,
                Parameters = [],
                ReturnParameters = [
                    new ReturnParameterModel { Name = "", Type = uint256Type, Index = 0 }
                ],
                Visibility = SolidityVisibilityEnum.Public,
                Mutability = SolidityFunctionMutabilityEnum.View,
                Body = FunctionBodyModel.Create(body => {
                    body.AddReturnStatement("100");
                })
            };
            functions.Add(publicViewFunction);

            var pureFunctionWithParams = new FunctionModel
            {
                Name = "calculateSum",
                FunctionType = SolidityFunctionTypeEnum.Normal,
                Parameters = [
                    new FunctionParameterModel { Name = "a", Type = uint256Type, Index = 0 },
                    new FunctionParameterModel { Name = "b", Type = uint256Type, Index = 1 }
                ],
                ReturnParameters = [
                    new ReturnParameterModel { Name = "", Type = uint256Type, Index = 0 }
                ],
                Visibility = SolidityVisibilityEnum.Public,
                Mutability = SolidityFunctionMutabilityEnum.Pure,
                Body = FunctionBodyModel.Create(body => {
                    body.AddReturnStatement("a + b");
                })
            };
            functions.Add(pureFunctionWithParams);

            var externalPayableFunction = new FunctionModel
            {
                Name = "deposit",
                FunctionType = SolidityFunctionTypeEnum.Normal,
                Parameters = [],
                ReturnParameters = [],
                Visibility = SolidityVisibilityEnum.External,
                Mutability = SolidityFunctionMutabilityEnum.Payable,
                Body = FunctionBodyModel.Create(body => {
                    body.AddEmitStatement("Deposit", "msg.sender", "msg.value");
                })
            };
            functions.Add(externalPayableFunction);

            var privateFunction = new FunctionModel
            {
                Name = "_validateAddress",
                FunctionType = SolidityFunctionTypeEnum.Normal,
                Parameters = [
                    new FunctionParameterModel { Name = "addr", Type = addressType, Index = 0 }
                ],
                ReturnParameters = [
                    new ReturnParameterModel { Name = "", Type = boolType, Index = 0 }
                ],
                Visibility = SolidityVisibilityEnum.Private,
                Body = FunctionBodyModel.Create(body => {
                    var ifStatement = body.AddIfStatement("addr == address(0)");
                    ifStatement.AddThenReturnStatement("false");
                    body.AddReturnStatement("true");
                })
            };
            functions.Add(privateFunction);

            var internalFunction = new FunctionModel
            {
                Name = "_processData",
                FunctionType = SolidityFunctionTypeEnum.Normal,
                Parameters = [
                    new FunctionParameterModel { Name = "data", Type = uint256Type, Index = 0 }
                ],
                ReturnParameters = [
                    new ReturnParameterModel { Name = "processedData", Type = uint256Type, Index = 0 }
                ],
                Visibility = SolidityVisibilityEnum.Internal,
                Modifiers = [
                    new ModifierModel { 
                        Name = "whenNotPaused", 
                        Body = "require(!paused, \"Contract is paused\");" 
                    }
                ],
                Body = FunctionBodyModel.Create(body => {
                    body.AddAssignment("uint256 result", "data * 2");
                    body.AddAssignment("result", "result + 10");
                    body.AddReturnStatement("result");
                })
            };
            functions.Add(internalFunction);

            var overrideFunction = new FunctionModel
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
                OverrideSpecifiers = new List<string> { "ERC165", "IERC1363" },
                Body = FunctionBodyModel.Create(body => {
                    var ifStatement = body.AddIfStatement("interfaceId == type(IERC1363).interfaceId");
                    ifStatement.AddThenReturnStatement("true");
                    body.AddReturnStatement("super.supportsInterface(interfaceId)");
                })
            };
            functions.Add(overrideFunction);

            var multipleReturnValues = new FunctionModel
            {
                Name = "getTokenInfo",
                FunctionType = SolidityFunctionTypeEnum.Normal,
                Parameters = [],
                ReturnParameters = [
                    new ReturnParameterModel { Name = "name", Type = stringType, Index = 0, Location = SolidityMemoryLocation.Memory },
                    new ReturnParameterModel { Name = "symbol", Type = stringType, Index = 1, Location = SolidityMemoryLocation.Memory },
                    new ReturnParameterModel { Name = "decimals", Type = uint8Type, Index = 2 }
                ],
                Visibility = SolidityVisibilityEnum.Public,
                Mutability = SolidityFunctionMutabilityEnum.View,
                Body = FunctionBodyModel.Create(body => {
                    body.AddReturnStatement("\"Test Token\"", "\"TST\"", "18");
                })
            };
            functions.Add(multipleReturnValues);

            var virtualFunction = new FunctionModel
            {
                Name = "_beforeTokenTransfer",
                FunctionType = SolidityFunctionTypeEnum.Normal,
                Parameters = [
                    new FunctionParameterModel { Name = "from", Type = addressType, Index = 0 },
                    new FunctionParameterModel { Name = "to", Type = addressType, Index = 1 },
                    new FunctionParameterModel { Name = "amount", Type = uint256Type, Index = 2 }
                ],
                Visibility = SolidityVisibilityEnum.Internal,
                IsVirtual = true,
                Body = FunctionBodyModel.Create(body => {
                    body.AddRequireStatement("from != address(0) || to != address(0)", "Invalid addresses");
                })
            };
            functions.Add(virtualFunction);

            return functions;
        }

        private List<FunctionModel> CreateSpecialFunctions(
            SimpleTypeReference uint256Type,
            SimpleTypeReference stringType,
            SimpleTypeReference addressType,
            SimpleTypeReference boolType,
            SimpleTypeReference bytesType)
        {
            var functions = new List<FunctionModel>();

            var fallbackFunction = new FunctionModel
            {
                FunctionType = SolidityFunctionTypeEnum.Fallback,
                Parameters = [
                    new FunctionParameterModel { 
                        Name = "input", 
                        Type = bytesType, 
                        Index = 0,
                        Location = SolidityMemoryLocation.Calldata 
                    }
                ],
                ReturnParameters = [
                    new ReturnParameterModel { 
                        Name = "output", 
                        Type = bytesType, 
                        Index = 0,
                        Location = SolidityMemoryLocation.Memory
                    }
                ],
                Visibility = SolidityVisibilityEnum.External,
                Mutability = SolidityFunctionMutabilityEnum.Payable,
                Body = FunctionBodyModel.Create(body => {
                    body.AddEmitStatement("FallbackCalled", "msg.sender", "msg.value", "input.length");
                    body.AddAssignment("bytes memory response", "new bytes(32)");
                    body.AddReturnStatement("response");
                })
            };
            functions.Add(fallbackFunction);

            var receiveFunction = new FunctionModel
            {
                FunctionType = SolidityFunctionTypeEnum.Receive,
                Visibility = SolidityVisibilityEnum.External,
                Mutability = SolidityFunctionMutabilityEnum.Payable,
                Body = FunctionBodyModel.Create(body => {
                    body.AddEmitStatement("Received", "msg.sender", "msg.value");
                    var ifStatement = body.AddIfStatement("msg.value > 0");
                    ifStatement.AddThenMethodCall("_processPayment", "msg.sender", "msg.value");
                })
            };
            functions.Add(receiveFunction);

            var interfaceFunction = new FunctionModel
            {
                Name = "transferFrom",
                FunctionType = SolidityFunctionTypeEnum.Normal,
                Parameters = [
                    new FunctionParameterModel { Name = "from", Type = addressType, Index = 0 },
                    new FunctionParameterModel { Name = "to", Type = addressType, Index = 1 },
                    new FunctionParameterModel { Name = "amount", Type = uint256Type, Index = 2 }
                ],
                ReturnParameters = [
                    new ReturnParameterModel { Name = "", Type = boolType, Index = 0 }
                ],
                Visibility = SolidityVisibilityEnum.External,
                IsInterfaceDeclaration = true
            };
            functions.Add(interfaceFunction);

            var errorHandlingFunction = new FunctionModel
            {
                Name = "processWithErrorHandling",
                FunctionType = SolidityFunctionTypeEnum.Normal,
                Parameters = [
                    new FunctionParameterModel { Name = "input", Type = uint256Type, Index = 0 }
                ],
                ReturnParameters = [
                    new ReturnParameterModel { Name = "", Type = boolType, Index = 0 }
                ],
                Visibility = SolidityVisibilityEnum.Public,
                CustomError = "ProcessingError",
                Body = FunctionBodyModel.Create(body => {
                    var ifStatement = body.AddIfStatement("input == 0");
                    ifStatement.AddThenStatement(new StatementInfo {
                        Type = StatementType.Revert,
                        Name = "InvalidInput",
                        Arguments = new List<string> { "input" }
                    });
                    
                    body.AddMethodCall("_complexProcessing", "input");
                    body.AddReturnStatement("true");
                })
            };
            functions.Add(errorHandlingFunction);

            var revertFunction = new FunctionModel
            {
                Name = "validateAndProcess",
                FunctionType = SolidityFunctionTypeEnum.Normal,
                Parameters = [
                    new FunctionParameterModel { Name = "value", Type = uint256Type, Index = 0 }
                ],
                Visibility = SolidityVisibilityEnum.Public,
                Body = FunctionBodyModel.Create(body => {
                    var ifZero = body.AddIfStatement("value == 0");
                    ifZero.AddThenStatement(new StatementInfo {
                        Type = StatementType.Revert,
                        Name = "ZeroValueNotAllowed"
                    });
                    
                    var ifLarge = body.AddIfStatement("value > 1000");
                    ifLarge.AddThenStatement(new StatementInfo {
                        Type = StatementType.Revert,
                        Name = "ValueTooHigh",
                        Arguments = new List<string> { "value", "1000" }
                    });
                    
                    body.AddMethodCall("_processValue", "value");
                })
            };
            functions.Add(revertFunction);

            return functions;
        }

        private List<FunctionModel> CreateComplexLogicFunctions(
            SimpleTypeReference uint256Type,
            SimpleTypeReference stringType,
            SimpleTypeReference addressType,
            SimpleTypeReference boolType)
        {
            var functions = new List<FunctionModel>();

            var nestedConditionsFunction = new FunctionModel
            {
                Name = "processWithNestedConditions",
                FunctionType = SolidityFunctionTypeEnum.Normal,
                Parameters = [
                    new FunctionParameterModel { Name = "amount", Type = uint256Type, Index = 0 },
                    new FunctionParameterModel { Name = "recipient", Type = addressType, Index = 1 }
                ],
                ReturnParameters = [
                    new ReturnParameterModel { Name = "", Type = boolType, Index = 0 }
                ],
                Visibility = SolidityVisibilityEnum.Public,
                Mutability = SolidityFunctionMutabilityEnum.Payable,
                Body = FunctionBodyModel.Create(body => {
                    var outerIf = body.AddIfStatement("msg.value > 0");
                    
                    var innerIf1 = new IfStatementInfo {
                        Type = StatementType.If,
                        Condition = "amount > 100"
                    };
                    innerIf1.AddThenMethodCall("_processHighValue", "amount", "recipient");
                    innerIf1.AddElseMethodCall("_processRegularValue", "amount", "recipient");
                    outerIf.ThenStatements.Add(innerIf1);
                    
                    var innerIf2 = new IfStatementInfo {
                        Type = StatementType.If,
                        Condition = "recipient == address(0)"
                    };
                    innerIf2.AddThenStatement(new StatementInfo {
                        Type = StatementType.Revert,
                        Name = "InvalidRecipient"
                    });
                    outerIf.ElseStatements.Add(innerIf2);
                    outerIf.AddElseMethodCall("_processZeroValue", "amount", "recipient");
                    
                    body.AddReturnStatement("true");
                })
            };
            functions.Add(nestedConditionsFunction);

            var complexMathFunction = new FunctionModel
            {
                Name = "calculateComplexFormula",
                FunctionType = SolidityFunctionTypeEnum.Normal,
                Parameters = [
                    new FunctionParameterModel { Name = "principal", Type = uint256Type, Index = 0 },
                    new FunctionParameterModel { Name = "rate", Type = uint256Type, Index = 1 },
                    new FunctionParameterModel { Name = "time", Type = uint256Type, Index = 2 }
                ],
                ReturnParameters = [
                    new ReturnParameterModel { Name = "simpleInterest", Type = uint256Type, Index = 0 },
                    new ReturnParameterModel { Name = "compoundInterest", Type = uint256Type, Index = 1 }
                ],
                Visibility = SolidityVisibilityEnum.Public,
                Mutability = SolidityFunctionMutabilityEnum.Pure,
                Body = FunctionBodyModel.Create(body => {
                    body.AddAssignment("uint256 _simpleInterest", "(principal * rate * time) / 10000");
                    body.AddAssignment("uint256 _compoundInterest", "principal");
                    body.AddAssignment("_compoundInterest", "_compoundInterest - principal");
                    body.AddReturnStatement("_simpleInterest", "_compoundInterest");
                })
            };
            functions.Add(complexMathFunction);

            var multiStepFunction = new FunctionModel
            {
                Name = "executeComplexTransaction",
                FunctionType = SolidityFunctionTypeEnum.Normal,
                Parameters = [
                    new FunctionParameterModel { Name = "recipient", Type = addressType, Index = 0 },
                    new FunctionParameterModel { Name = "amount", Type = uint256Type, Index = 1 }
                ],
                ReturnParameters = [
                    new ReturnParameterModel { Name = "", Type = boolType, Index = 0 }
                ],
                Visibility = SolidityVisibilityEnum.Public,
                Body = FunctionBodyModel.Create(body => {
                    body.AddRequireStatement("recipient != address(0)", "Invalid recipient address");
                    body.AddRequireStatement("amount > 0", "Amount must be greater than zero");
                    
                    var ifStep2 = body.AddIfStatement("balanceOf(msg.sender) < amount");
                    ifStep2.AddThenStatement(new StatementInfo {
                        Type = StatementType.Revert,
                        Name = "InsufficientBalance",
                        Arguments = new List<string> { "balanceOf(msg.sender)", "amount" }
                    });
                    
                    body.AddEmitStatement("TransactionInitiated", "msg.sender", "recipient", "amount");
                    body.AddMethodCall("_transfer", "msg.sender", "recipient", "amount");
                    
                    var ifStep5 = body.AddIfStatement("_feePercentage > 0");
                    ifStep5.AddThenAssignment("uint256 fee", "(amount * _feePercentage) / 10000");
                    ifStep5.AddThenMethodCall("_processFee", "fee");
                    
                    body.AddEmitStatement("TransactionCompleted", "msg.sender", "recipient", "amount");
                    body.AddReturnStatement("true");
                })
            };
            functions.Add(multiStepFunction);

            return functions;
        }

        private List<FunctionModel> CreateEdgeCaseFunctions(
            SimpleTypeReference uint256Type,
            SimpleTypeReference stringType,
            SimpleTypeReference addressType,
            SimpleTypeReference boolType)
        {
            var functions = new List<FunctionModel>();

            var longNameFunction = new FunctionModel
            {
                Name = "processDataAndUpdateStateAndNotifyUsersAndLogTransactionDetails",
                FunctionType = SolidityFunctionTypeEnum.Normal,
                Parameters = [
                    new FunctionParameterModel { Name = "data", Type = uint256Type, Index = 0 }
                ],
                Visibility = SolidityVisibilityEnum.Public,
                Body = FunctionBodyModel.Create(body => {
                    body.AddMethodCall("_processData", "data");
                })
            };
            functions.Add(longNameFunction);

            var emptyBodyFunction = new FunctionModel
            {
                Name = "emptyFunction",
                FunctionType = SolidityFunctionTypeEnum.Normal,
                Parameters = [],
                Visibility = SolidityVisibilityEnum.Public,
                Body = FunctionBodyModel.Create(body => {
                })
            };
            functions.Add(emptyBodyFunction);

            var longCommentFunction = new FunctionModel
            {
                Name = "functionWithLongComment",
                FunctionType = SolidityFunctionTypeEnum.Normal,
                Parameters = [],
                Visibility = SolidityVisibilityEnum.Public,
                Body = FunctionBodyModel.Create(body => {
                    body.AddMethodCall("_doNothing");
                })
            };
            functions.Add(longCommentFunction);

            var customErrorFunction = new FunctionModel
            {
                Name = "functionWithCustomErrors",
                FunctionType = SolidityFunctionTypeEnum.Normal,
                Parameters = [
                    new FunctionParameterModel { Name = "value", Type = uint256Type, Index = 0 }
                ],
                ReturnParameters = [
                    new ReturnParameterModel { Name = "", Type = boolType, Index = 0 }
                ],
                Visibility = SolidityVisibilityEnum.Public,
                CustomError = "CustomError",
                Body = FunctionBodyModel.Create(body => {
                    var ifStatement = body.AddIfStatement("value == 0");
                    ifStatement.AddThenStatement(new StatementInfo {
                        Type = StatementType.Revert,
                        Name = "DataError",
                        Arguments = new List<string> { "value" }
                    });
                    
                    body.AddMethodCall("_processValue", "value");
                    body.AddReturnStatement("true");
                })
            };
            functions.Add(customErrorFunction);

            var manyParamsFunction = new FunctionModel
            {
                Name = "functionWithManyParameters",
                FunctionType = SolidityFunctionTypeEnum.Normal,
                Parameters = [
                    new FunctionParameterModel { Name = "param1", Type = uint256Type, Index = 0 },
                    new FunctionParameterModel { Name = "param2", Type = uint256Type, Index = 1 },
                    new FunctionParameterModel { Name = "param3", Type = stringType, Index = 2, Location = SolidityMemoryLocation.Memory },
                    new FunctionParameterModel { Name = "param4", Type = addressType, Index = 3 },
                    new FunctionParameterModel { Name = "param5", Type = boolType, Index = 4 },
                    new FunctionParameterModel { Name = "param6", Type = uint256Type, Index = 5 },
                    new FunctionParameterModel { Name = "param7", Type = uint256Type, Index = 6 },
                    new FunctionParameterModel { Name = "param8", Type = stringType, Index = 7, Location = SolidityMemoryLocation.Memory }
                ],
                Visibility = SolidityVisibilityEnum.Public,
                Body = FunctionBodyModel.Create(body => {
                    body.AddMethodCall("_processMultipleParams");
                })
            };
            functions.Add(manyParamsFunction);

            var manyModifiersFunction = new FunctionModel
            {
                Name = "functionWithManyModifiers",
                FunctionType = SolidityFunctionTypeEnum.Normal,
                Parameters = [],
                Visibility = SolidityVisibilityEnum.Public,
                Modifiers = [
                    new ModifierModel { Name = "onlyOwner", Body = "require(msg.sender == owner());" },
                    new ModifierModel { Name = "whenNotPaused", Body = "require(!paused);" },
                    new ModifierModel { Name = "nonReentrant", Body = "_nonReentrantBefore(); _; _nonReentrantAfter();" },
                    new ModifierModel { 
                        Name = "validPeriod", 
                        Arguments = new List<string> { "block.timestamp", "endTime" },
                        Body = "require(a < b);" 
                    },
                    new ModifierModel { Name = "onlyRole", 
                        Arguments = new List<string> { "ADMIN_ROLE" },
                        Body = "require(hasRole(role, msg.sender));" 
                    }
                ],
                Body = FunctionBodyModel.Create(body => {
                    body.AddMethodCall("_adminAction");
                })
            };
            functions.Add(manyModifiersFunction);

            return functions;
        }
    }
}