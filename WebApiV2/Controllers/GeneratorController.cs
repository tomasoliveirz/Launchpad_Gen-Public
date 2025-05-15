using Microsoft.AspNetCore.Mvc;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Errors;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Events;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Header;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Imports;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Modifiers;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Parameters;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.State;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.TypeReferences;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors;
using Moongy.RD.Launchpad.Core.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Functions;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements.Expressions;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Version;
using System.Diagnostics.Contracts;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Prebuilds;

namespace WebApiV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneratorController : ControllerBase
    {

        [HttpGet("generate-prebuilds")]
        public ActionResult GeneratePrebuilds()

        {
            #region File Header
            // Create file header
            var minVersion = new SoftwareVersion { Major = 0, Minor = 8, Revision = 20 };
            var maxVersion = new SoftwareVersion { Major = 0, Minor = 8, Revision = 20 };
            var fileHeader = new FileHeaderModel
            {
                License = SpdxLicense.MIT,
                Version = new VersionModel { Minimum = minVersion, Maximum = maxVersion }
            };
            #endregion

            #region Contract
            var permitContract = new SolidityContractModel
            {
                Name = "Permit",
                Errors = new List<ErrorModel>
        {
            PermitPrebuild.ERC2612ExpiredSignature,
            PermitPrebuild.ERC2612InvalidSigner
        },
                ConstructorParameters = new List<ConstructorParameterModel>
        {
            PermitPrebuild.EIP712ConstructorParameter
        }
            };
            #endregion

            #region File
            var file = new SolidityFile
            {
                FileHeader = fileHeader,
                Contracts = [permitContract]
            };
            #endregion

            #region Render
            // Generate complete Solidity code
            var solidityCode = GenerateSolidityCode(file);
            #endregion

            return Ok(solidityCode);
        }

        [HttpGet("generate-erc20")]
        public ActionResult GenerateERC20Contract()
        {
            #region Types
            // Define basic types
            var uint256Type = new SimpleTypeReference(SolidityDataTypeEnum.Uint256);
            var uint8Type = new SimpleTypeReference(SolidityDataTypeEnum.Uint8);
            var stringType = new SimpleTypeReference(SolidityDataTypeEnum.String);
            var addressType = new SimpleTypeReference(SolidityDataTypeEnum.Address);
            var boolType = new SimpleTypeReference(SolidityDataTypeEnum.Bool);

            // Define array types
            var addressArrayType = new ArrayTypeReference(addressType);
            var uint256ArrayType = new ArrayTypeReference(uint256Type);

            // Define mapping types
            var balancesType = new MappingTypeReference(addressType, uint256Type);
            var allowancesType = new MappingTypeReference(addressType, 
                                new MappingTypeReference(addressType, uint256Type));
            #endregion

            #region State Properties
            // Basic token properties
            var nameProperty = new StatePropertyModel 
            { 
                Name = "_name", 
                Type = stringType, 
                Visibility = SolidityVisibilityEnum.Private
            };

            var symbolProperty = new StatePropertyModel 
            { 
                Name = "_symbol", 
                Type = stringType, 
                Visibility = SolidityVisibilityEnum.Private
            };

            var decimalsProperty = new StatePropertyModel 
            { 
                Name = "_decimals", 
                Type = uint8Type, 
                Visibility = SolidityVisibilityEnum.Private,
                IsConstant = true,
                InitialValue = "18"
            };

            var totalSupplyProperty = new StatePropertyModel 
            { 
                Name = "_totalSupply", 
                Type = uint256Type, 
                Visibility = SolidityVisibilityEnum.Private
            };

            var balancesProperty = new StatePropertyModel 
            { 
                Name = "_balances", 
                Type = balancesType, 
                Visibility = SolidityVisibilityEnum.Private
            };

            var allowancesProperty = new StatePropertyModel 
            { 
                Name = "_allowances", 
                Type = allowancesType, 
                Visibility = SolidityVisibilityEnum.Private
            };

            var ownerProperty = new StatePropertyModel 
            { 
                Name = "_owner", 
                Type = addressType, 
                Visibility = SolidityVisibilityEnum.Private
            };

            var feeRateProperty = new StatePropertyModel
            {
                Name = "_feeRate",
                Type = uint256Type,
                Visibility = SolidityVisibilityEnum.Private,
                InitialValue = "0"
            };

            var pausedProperty = new StatePropertyModel
            {
                Name = "_paused",
                Type = boolType,
                Visibility = SolidityVisibilityEnum.Private,
                InitialValue = "false"
            };

            var stateProperties = new List<StatePropertyModel>
            {
                nameProperty,
                symbolProperty,
                decimalsProperty,
                totalSupplyProperty,
                balancesProperty,
                allowancesProperty,
                ownerProperty,
                feeRateProperty,
                pausedProperty
            };
            #endregion

            #region Events
            // Transfer event
            var fromParam = new EventParameterModel 
            { 
                Name = "from", 
                Type = addressType, 
                IsIndexed = true 
            };
            
            var toParam = new EventParameterModel 
            { 
                Name = "to", 
                Type = addressType, 
                IsIndexed = true 
            };
            
            var valueParam = new EventParameterModel 
            { 
                Name = "value", 
                Type = uint256Type 
            };
            
            var transferEvent = new EventModel 
            { 
                Name = "Transfer", 
                Parameters = new List<EventParameterModel> { fromParam, toParam, valueParam } 
            };

            // Approval event
            var ownerParam = new EventParameterModel 
            { 
                Name = "owner", 
                Type = addressType, 
                IsIndexed = true 
            };
            
            var spenderParam = new EventParameterModel 
            { 
                Name = "spender", 
                Type = addressType, 
                IsIndexed = true 
            };
            
            var approvalEvent = new EventModel 
            { 
                Name = "Approval", 
                Parameters = new List<EventParameterModel> { ownerParam, spenderParam, valueParam } 
            };

            // Fee changed event
            var oldFeeParam = new EventParameterModel
            {
                Name = "oldFee",
                Type = uint256Type
            };

            var newFeeParam = new EventParameterModel
            {
                Name = "newFee",
                Type = uint256Type
            };

            var feeChangedEvent = new EventModel
            {
                Name = "FeeChanged",
                Parameters = new List<EventParameterModel> { oldFeeParam, newFeeParam }
            };

            // Paused events
            var pausedEvent = new EventModel
            {
                Name = "Paused",
                Parameters = new List<EventParameterModel> { }
            };

            var unpausedEvent = new EventModel
            {
                Name = "Unpaused",
                Parameters = new List<EventParameterModel> { }
            };

            var events = new List<EventModel> 
            { 
                transferEvent, 
                approvalEvent, 
                feeChangedEvent,
                pausedEvent,
                unpausedEvent
            };
            #endregion

            #region Errors
            var addressErrorParam = new ErrorParameterModel 
            { 
                Name = "account", 
                Type = addressType 
            };
            
            var zeroAddressError = new ErrorModel 
            { 
                Name = "ZeroAddress", 
                Parameters = new List<ErrorParameterModel> { addressErrorParam } 
            };

            var amountErrorParam = new ErrorParameterModel 
            { 
                Name = "available", 
                Type = uint256Type 
            };
            
            var requiredErrorParam = new ErrorParameterModel 
            { 
                Name = "required", 
                Type = uint256Type 
            };
            
            var insufficientBalanceError = new ErrorModel 
            { 
                Name = "InsufficientBalance", 
                Parameters = new List<ErrorParameterModel> { amountErrorParam, requiredErrorParam } 
            };

            var pausedError = new ErrorModel
            {
                Name = "Paused",
                Parameters = new List<ErrorParameterModel> { }
            };

            var notOwnerError = new ErrorModel
            {
                Name = "NotOwner",
                Parameters = new List<ErrorParameterModel> { }
            };

            var transferFailedError = new ErrorModel
            {
                Name = "TransferFailed",
                Parameters = new List<ErrorParameterModel> { }
            };

            var errors = new List<ErrorModel> 
            { 
                zeroAddressError, 
                insufficientBalanceError,
                pausedError,
                notOwnerError,
                transferFailedError
            };
            #endregion

            #region Modifiers
            var onlyOwnerModifier = new ModifierModel 
            { 
                Name = "onlyOwner", 
                Body = "require(msg.sender == _owner, \"Caller is not the owner\");\n_;" 
            };

            var whenNotPausedModifier = new ModifierModel
            {
                Name = "whenNotPaused",
                Body = "require(!_paused, \"Contract is paused\");\n_;"
            };

            var whenPausedModifier = new ModifierModel
            {
                Name = "whenPaused",
                Body = "require(_paused, \"Contract is not paused\");\n_;"
            };

            var modifiers = new List<ModifierModel> 
            { 
                onlyOwnerModifier, 
                whenNotPausedModifier,
                whenPausedModifier
            };
            #endregion

            #region Constructor
            var nameParameter = new ConstructorParameterModel 
            { 
                Name = "name_", 
                Type = stringType, 
                Index = 0,
                AssignedTo = "_name"
            };
            
            var symbolParameter = new ConstructorParameterModel 
            { 
                Name = "symbol_", 
                Type = stringType, 
                Index = 1,
                AssignedTo = "_symbol"
            };
            
            var initialOwnerParameter = new ConstructorParameterModel 
            { 
                Name = "initialOwner", 
                Type = addressType, 
                Index = 2,
                AssignedTo = "_owner"
            };

            var constructorParameters = new List<ConstructorParameterModel>
            {
                nameParameter,
                symbolParameter,
                initialOwnerParameter
            };
            #endregion

            #region Functions
            // Create functions for the ERC20 token
            var functions = new List<BaseFunctionModel>();
            
            // Standard ERC20 functions
            var mintFunction = CreateMintFunction(addressType, uint256Type, onlyOwnerModifier);
            functions.Add(mintFunction);
            
            var transferFunction = CreateTransferFunction(addressType, uint256Type, boolType, whenNotPausedModifier);
            functions.Add(transferFunction);
            
            var approveFunction = CreateApproveFunction(addressType, uint256Type, boolType, whenNotPausedModifier);
            functions.Add(approveFunction);
            
            var transferFromFunction = CreateTransferFromFunction(addressType, uint256Type, boolType, whenNotPausedModifier);
            functions.Add(transferFromFunction);
            
            var balanceOfFunction = CreateBalanceOfFunction(addressType, uint256Type);
            functions.Add(balanceOfFunction);
            
            var allowanceFunction = CreateAllowanceFunction(addressType, uint256Type);
            functions.Add(allowanceFunction);
            
            var totalSupplyFunction = CreateTotalSupplyFunction(uint256Type);
            functions.Add(totalSupplyFunction);
            
            var nameFunction = CreateSimpleGetterFunction("name", "_name", stringType);
            functions.Add(nameFunction);
            
            var symbolFunction = CreateSimpleGetterFunction("symbol", "_symbol", stringType);
            functions.Add(symbolFunction);
            
            var decimalsFunction = CreateSimpleGetterFunction("decimals", "_decimals", uint8Type);
            functions.Add(decimalsFunction);
            
            // Additional functions with different statements
            var batchTransferFunction = CreateBatchTransferFunction(addressArrayType, uint256ArrayType, uint256Type, boolType, whenNotPausedModifier);
            functions.Add(batchTransferFunction);
            
            var burnFunction = CreateBurnFunction(uint256Type, boolType, whenNotPausedModifier);
            functions.Add(burnFunction);
            
            var setFeeFunction = CreateSetFeeFunction(uint256Type, onlyOwnerModifier);
            functions.Add(setFeeFunction);
            
            var pauseFunction = CreatePauseFunction(onlyOwnerModifier, whenNotPausedModifier);
            functions.Add(pauseFunction);
            
            var unpauseFunction = CreateUnpauseFunction(onlyOwnerModifier, whenPausedModifier);
            functions.Add(unpauseFunction);
            
            var transferWithFeeFunction = CreateTransferWithFeeFunction(addressType, uint256Type, boolType, whenNotPausedModifier);
            functions.Add(transferWithFeeFunction);
            
            var safeTransferFunction = CreateSafeTransferFunction(addressType, uint256Type, boolType, whenNotPausedModifier);
            functions.Add(safeTransferFunction);
            #endregion

            #region Contract
            // Create the contract model
            var contract = new SolidityContractModel
            {
                Name = "EnhancedERC20",
                StateProperties = stateProperties,
                Events = events,
                Errors = errors,
                Modifiers = modifiers,
                ConstructorParameters = constructorParameters,
                Functions = functions
            };
            #endregion

            #region File Header
            // Create file header
            var minVersion = new SoftwareVersion { Major = 0, Minor = 8, Revision = 20 };
            var maxVersion = new SoftwareVersion { Major = 0, Minor = 8, Revision = 20 };
            var fileHeader = new FileHeaderModel
            {
                License = SpdxLicense.MIT,
                Version = new VersionModel { Minimum = minVersion, Maximum = maxVersion }
            };
            #endregion

            #region File
            // Create the Solidity file
            var file = new SolidityFile
            {
                FileHeader = fileHeader,
                Contracts = new[] { contract }
            };
            #endregion

            #region Render
            // Generate complete Solidity code
            var solidityCode = GenerateSolidityCode(file);
            #endregion

            return Ok(solidityCode);
        }

        #region Helper Methods for Creating Functions
        
        private NormalFunctionModel CreateApproveFunction(TypeReference addressType, TypeReference uint256Type, TypeReference boolType, ModifierModel whenNotPausedModifier)
        {
            // Parameters
            var spenderParam = new FunctionParameterModel
            {
                Name = "spender",
                Type = addressType,
                Index = 0
            };

            var amountParam = new FunctionParameterModel
            {
                Name = "amount",
                Type = uint256Type,
                Index = 1
            };

            // Return parameter
            var successParam = new ReturnParameterModel
            {
                Name = "success",
                Type = boolType,
                Index = 0
            };

            // Create function
            var approveFunction = new NormalFunctionModel
            {
                Name = "approve",
                Visibility = SolidityVisibilityEnum.External,
                Parameters = new List<FunctionParameterModel> { spenderParam, amountParam },
                ReturnParameters = new List<ReturnParameterModel> { successParam }
            };
            
            // Add modifier
            approveFunction.Modifiers.Add(whenNotPausedModifier);

            // Create expressions for require
            var spenderExpr = new LiteralExpressionModel("spender");
            var zeroAddressExpr = new LiteralExpressionModel("address(0)");
            var notZeroAddressExpr = spenderExpr.NotEqual(zeroAddressExpr);
            
            // Add require statement
            approveFunction.AddStatement(new RequireStatement { 
                Condition = notZeroAddressExpr, 
                Message = "Cannot approve zero address" 
            });

            // Add assignment statement
            approveFunction.AddStatement(new AssignmentStatement { 
                Target = "_allowances[msg.sender][spender]", 
                Value = "amount" 
            });

            // Add emit statement
            var emitStmt = new EmitStatement("Approval");
            emitStmt.Arguments.AddRange(new[] { "msg.sender", "spender", "amount" });
            approveFunction.AddStatement(emitStmt);

            // Add return statement
            approveFunction.AddStatement(new ReturnStatement("true"));

            return approveFunction;
        }

        private NormalFunctionModel CreateTransferFromFunction(TypeReference addressType, TypeReference uint256Type, TypeReference boolType, ModifierModel whenNotPausedModifier)
        {
            // Parameters
            var fromParam = new FunctionParameterModel
            {
                Name = "from",
                Type = addressType,
                Index = 0
            };

            var toParam = new FunctionParameterModel
            {
                Name = "to",
                Type = addressType,
                Index = 1
            };

            var amountParam = new FunctionParameterModel
            {
                Name = "amount",
                Type = uint256Type,
                Index = 2
            };

            // Return parameter
            var successParam = new ReturnParameterModel
            {
                Name = "success",
                Type = boolType,
                Index = 0
            };

            // Create function
            var transferFromFunction = new NormalFunctionModel
            {
                Name = "transferFrom",
                Visibility = SolidityVisibilityEnum.External,
                Parameters = new List<FunctionParameterModel> { fromParam, toParam, amountParam },
                ReturnParameters = new List<ReturnParameterModel> { successParam }
            };
            
            // Add modifier
            transferFromFunction.Modifiers.Add(whenNotPausedModifier);

            // Create expressions for require statements
            var fromExpr = new LiteralExpressionModel("from");
            var toExpr = new LiteralExpressionModel("to");
            var zeroAddressExpr = new LiteralExpressionModel("address(0)");
            var fromNotZeroExpr = fromExpr.NotEqual(zeroAddressExpr);
            var toNotZeroExpr = toExpr.NotEqual(zeroAddressExpr);
            
            var amountExpr = new LiteralExpressionModel("amount");
            var fromBalanceExpr = new LiteralExpressionModel("_balances[from]");
            var allowanceExpr = new LiteralExpressionModel("_allowances[from][msg.sender]");
            var sufficientBalanceExpr = amountExpr.LessOrEqual(fromBalanceExpr);
            var sufficientAllowanceExpr = amountExpr.LessOrEqual(allowanceExpr);
            
            // Add require statements
            transferFromFunction.AddStatement(new RequireStatement { 
                Condition = fromNotZeroExpr, 
                Message = "Cannot transfer from zero address" 
            });
            
            transferFromFunction.AddStatement(new RequireStatement { 
                Condition = toNotZeroExpr, 
                Message = "Cannot transfer to zero address" 
            });
            
            transferFromFunction.AddStatement(new RequireStatement { 
                Condition = sufficientBalanceExpr, 
                Message = "Insufficient balance" 
            });
            
            transferFromFunction.AddStatement(new RequireStatement { 
                Condition = sufficientAllowanceExpr, 
                Message = "Insufficient allowance" 
            });

            // Add variable declaration for current allowance
            transferFromFunction.AddStatement(new VariableDeclarationStatement(
                uint256Type, 
                "currentAllowance", 
                "_allowances[from][msg.sender]", 
                null)
            );

            // Add assignment statements
            transferFromFunction.AddStatement(new AssignmentStatement { 
                Target = "_balances[from]", 
                Value = "_balances[from] - amount" 
            });
            
            transferFromFunction.AddStatement(new AssignmentStatement { 
                Target = "_balances[to]", 
                Value = "_balances[to] + amount" 
            });
            
            transferFromFunction.AddStatement(new AssignmentStatement { 
                Target = "_allowances[from][msg.sender]", 
                Value = "currentAllowance - amount" 
            });

            // Add emit statement
            var emitStmt = new EmitStatement("Transfer");
            emitStmt.Arguments.AddRange(new[] { "from", "to", "amount" });
            transferFromFunction.AddStatement(emitStmt);

            // Add return statement
            transferFromFunction.AddStatement(new ReturnStatement("true"));

            return transferFromFunction;
        }

        private NormalFunctionModel CreateBalanceOfFunction(TypeReference addressType, TypeReference uint256Type)
        {
            // Parameter
            var accountParam = new FunctionParameterModel
            {
                Name = "account",
                Type = addressType,
                Index = 0
            };

            // Return parameter
            var balanceParam = new ReturnParameterModel
            {
                Name = "balance",
                Type = uint256Type,
                Index = 0
            };

            // Create function
            var balanceOfFunction = new NormalFunctionModel
            {
                Name = "balanceOf",
                Visibility = SolidityVisibilityEnum.External,
                Mutability = SolidityFunctionMutabilityEnum.View,
                Parameters = new List<FunctionParameterModel> { accountParam },
                ReturnParameters = new List<ReturnParameterModel> { balanceParam }
            };

            // Add return statement
            var returnStmt = new ReturnStatement();
            returnStmt.Values.Add("_balances[account]");
            balanceOfFunction.AddStatement(returnStmt);

            return balanceOfFunction;
        }

        private NormalFunctionModel CreateAllowanceFunction(TypeReference addressType, TypeReference uint256Type)
        {
            // Parameters
            var ownerParam = new FunctionParameterModel
            {
                Name = "owner",
                Type = addressType,
                Index = 0
            };

            var spenderParam = new FunctionParameterModel
            {
                Name = "spender",
                Type = addressType,
                Index = 1
            };

            // Return parameter
            var allowanceParam = new ReturnParameterModel
            {
                Name = "remaining",
                Type = uint256Type,
                Index = 0
            };

            // Create function
            var allowanceFunction = new NormalFunctionModel
            {
                Name = "allowance",
                Visibility = SolidityVisibilityEnum.External,
                Mutability = SolidityFunctionMutabilityEnum.View,
                Parameters = new List<FunctionParameterModel> { ownerParam, spenderParam },
                ReturnParameters = new List<ReturnParameterModel> { allowanceParam }
            };

            // Add return statement
            var returnStmt = new ReturnStatement();
            returnStmt.Values.Add("_allowances[owner][spender]");
            allowanceFunction.AddStatement(returnStmt);

            return allowanceFunction;
        }

        private NormalFunctionModel CreateTotalSupplyFunction(TypeReference uint256Type)
        {
            // Return parameter
            var totalSupplyParam = new ReturnParameterModel
            {
                Name = "supply",
                Type = uint256Type,
                Index = 0
            };

            // Create function
            var totalSupplyFunction = new NormalFunctionModel
            {
                Name = "totalSupply",
                Visibility = SolidityVisibilityEnum.External,
                Mutability = SolidityFunctionMutabilityEnum.View,
                ReturnParameters = new List<ReturnParameterModel> { totalSupplyParam }
            };

            // Add return statement
            var returnStmt = new ReturnStatement();
            returnStmt.Values.Add("_totalSupply");
            totalSupplyFunction.AddStatement(returnStmt);

            return totalSupplyFunction;
        }

        private NormalFunctionModel CreateSimpleGetterFunction(string functionName, string propertyName, TypeReference returnType)
        {
            // Return parameter
            var returnParam = new ReturnParameterModel
            {
                Name = "value",
                Type = returnType,
                Index = 0
            };

            // Create function
            var getterFunction = new NormalFunctionModel
            {
                Name = functionName,
                Visibility = SolidityVisibilityEnum.External,
                Mutability = SolidityFunctionMutabilityEnum.View,
                ReturnParameters = new List<ReturnParameterModel> { returnParam }
            };

            // Add return statement
            var returnStmt = new ReturnStatement();
            returnStmt.Values.Add(propertyName);
            getterFunction.AddStatement(returnStmt);

            return getterFunction;
        }
        
        // Function using a for loop to process arrays
        private NormalFunctionModel CreateBatchTransferFunction(TypeReference addressArrayType, TypeReference uint256ArrayType, TypeReference uint256Type, TypeReference boolType, ModifierModel whenNotPausedModifier)
        {
            // Parameters
            var recipientsParam = new FunctionParameterModel
            {
                Name = "recipients",
                Type = addressArrayType,
                Index = 0,
                Location = SolidityMemoryLocation.Memory
            };

            var amountsParam = new FunctionParameterModel
            {
                Name = "amounts",
                Type = uint256ArrayType,
                Index = 1,
                Location = SolidityMemoryLocation.Memory
            };

            // Return parameter
            var successParam = new ReturnParameterModel
            {
                Name = "success",
                Type = boolType,
                Index = 0
            };

            // Create function
            var batchTransferFunction = new NormalFunctionModel
            {
                Name = "batchTransfer",
                Visibility = SolidityVisibilityEnum.External,
                Parameters = new List<FunctionParameterModel> { recipientsParam, amountsParam },
                ReturnParameters = new List<ReturnParameterModel> { successParam }
            };
            
            // Add modifier
            batchTransferFunction.Modifiers.Add(whenNotPausedModifier);

            // Create expressions for require statements
            var recipientsLengthExpr = new LiteralExpressionModel("recipients.length");
            var amountsLengthExpr = new LiteralExpressionModel("amounts.length");
            var zeroExpr = new LiteralExpressionModel("0");
            
            var lengthsEqualExpr = recipientsLengthExpr.Equal(amountsLengthExpr);
            var lengthsNotEmptyExpr = recipientsLengthExpr.GreaterThan(zeroExpr);
            
            // Add require statements
            batchTransferFunction.AddStatement(new RequireStatement { 
                Condition = lengthsEqualExpr, 
                Message = "Arrays length mismatch" 
            });
            
            batchTransferFunction.AddStatement(new RequireStatement { 
                Condition = lengthsNotEmptyExpr, 
                Message = "Empty arrays" 
            });

            // Declare variable for total amount
            batchTransferFunction.AddStatement(new VariableDeclarationStatement(
                uint256Type, 
                "totalAmount", 
                "0", 
                null)
            );

            // First loop initialization
            var initExpr1 = new VariableDeclarationStatement(
                uint256Type, 
                "i", 
                "0", 
                null
            );
            
            // First loop condition
            var iExpr = new LiteralExpressionModel("i");
            var conditionExpr1 = iExpr.LessThan(amountsLengthExpr);
            
            // First loop iterator
            var iteratorExpr1 = new AssignmentStatement { 
                Target = "i", 
                Value = "i + 1" 
            };
            
            // Create first for loop
            var firstForLoop = new ForStatement(initExpr1, conditionExpr1, iteratorExpr1);
            
            // Add statements to first loop body
            var amountAtIExpr = new LiteralExpressionModel("amounts[i]");
            var amountGreaterZeroExpr = amountAtIExpr.GreaterThan(zeroExpr);
            
            firstForLoop.AddBodyStatement(new RequireStatement { 
                Condition = amountGreaterZeroExpr, 
                Message = "Zero amount" 
            });
            
            firstForLoop.AddBodyStatement(new AssignmentStatement { 
                Target = "totalAmount", 
                Value = "totalAmount + amounts[i]" 
            });
            
            // Add first loop to function
            batchTransferFunction.AddStatement(firstForLoop);
            
            // Check if sender has enough balance
            var totalAmountExpr = new LiteralExpressionModel("totalAmount");
            var senderBalanceExpr = new LiteralExpressionModel("_balances[msg.sender]");
            var sufficientBalanceExpr = totalAmountExpr.LessOrEqual(senderBalanceExpr);
            
            batchTransferFunction.AddStatement(new RequireStatement { 
                Condition = sufficientBalanceExpr, 
                Message = "Insufficient balance" 
            });
            
            // Second loop initialization
            var initExpr2 = new VariableDeclarationStatement(
                uint256Type, 
                "i", 
                "0", 
                null
            );
            
            // Second loop condition
            var recipientsLengthExpr2 = new LiteralExpressionModel("recipients.length");
            var conditionExpr2 = iExpr.LessThan(recipientsLengthExpr2);
            
            // Second loop iterator
            var iteratorExpr2 = new AssignmentStatement { 
                Target = "i", 
                Value = "i + 1" 
            };
            
            // Create second for loop
            var secondForLoop = new ForStatement(initExpr2, conditionExpr2, iteratorExpr2);
            
            // Add statements to second loop body
            var recipientAtIExpr = new LiteralExpressionModel("recipients[i]");
            var recipientNotZeroExpr = recipientAtIExpr.NotEqual(new LiteralExpressionModel("address(0)"));
            
            secondForLoop.AddBodyStatement(new RequireStatement { 
                Condition = recipientNotZeroExpr, 
                Message = "Zero address" 
            });
            
            secondForLoop.AddBodyStatement(new AssignmentStatement { 
                Target = "_balances[msg.sender]", 
                Value = "_balances[msg.sender] - amounts[i]" 
            });
            
            secondForLoop.AddBodyStatement(new AssignmentStatement { 
                Target = "_balances[recipients[i]]", 
                Value = "_balances[recipients[i]] + amounts[i]" 
            });
            
            // Use this method to build the emit statement to avoid ambiguity
            var emitTransfer = new EmitStatement("Transfer");
            emitTransfer.Arguments.Add("msg.sender");
            emitTransfer.Arguments.Add("recipients[i]");
            emitTransfer.Arguments.Add("amounts[i]");
            secondForLoop.AddBodyStatement(emitTransfer);
            
            // Add second loop to function
            batchTransferFunction.AddStatement(secondForLoop);
            
            // Return true
            batchTransferFunction.AddStatement(new ReturnStatement("true"));
            
            return batchTransferFunction;
        }
        
        private NormalFunctionModel CreateMintFunction(TypeReference addressType, TypeReference uint256Type, ModifierModel onlyOwnerModifier) {
            // Parameters
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

            // Create function
            var mintFunction = new NormalFunctionModel
            {
                Name = "mint",
                Visibility = SolidityVisibilityEnum.Public,
                Parameters = new List<FunctionParameterModel> { toParam, amountParam }
            };

            // Add onlyOwner modifier
            mintFunction.Modifiers.Add(onlyOwnerModifier);

            // Add require statements using ExpressionModel
            // require(to != address(0), "Cannot mint to zero address");
            var toExpr = new LiteralExpressionModel("to");
            var zeroAddressExpr = new LiteralExpressionModel("address(0)");
            var notZeroAddressExpr = toExpr.NotEqual(zeroAddressExpr);
            
            mintFunction.AddStatement(new RequireStatement { 
                Condition = notZeroAddressExpr, 
                Message = "Cannot mint to zero address" 
            });

            // require(amount > 0, "Amount must be greater than zero");
            var amountExpr = new LiteralExpressionModel("amount");
            var zeroExpr = new LiteralExpressionModel("0");
            var amountGreaterZeroExpr = amountExpr.GreaterThan(zeroExpr);
            
            mintFunction.AddStatement(new RequireStatement { 
                Condition = amountGreaterZeroExpr, 
                Message = "Amount must be greater than zero" 
            });

            // Add assignment statements
            // _balances[to] = _balances[to] + amount;
            mintFunction.AddStatement(new AssignmentStatement { 
                Target = "_balances[to]", 
                Value = "_balances[to] + amount" 
            });

            // _totalSupply = _totalSupply + amount;
            mintFunction.AddStatement(new AssignmentStatement { 
                Target = "_totalSupply", 
                Value = "_totalSupply + amount" 
            });

            // Add emit statement
            var emitTransfer = new EmitStatement("Transfer")
                .AddStringArgument("address(0)")
                .AddStringArgument("to")
                .AddStringArgument("amount");
                
            mintFunction.AddStatement(emitTransfer);

            return mintFunction;
        }

        private NormalFunctionModel CreateTransferFunction(TypeReference addressType, TypeReference uint256Type, TypeReference boolType, ModifierModel whenNotPausedModifier)
        {
            // Parameters
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

            // Return parameter
            var successParam = new ReturnParameterModel
            {
                Name = "success",
                Type = boolType,
                Index = 0
            };

            // Create function
            var transferFunction = new NormalFunctionModel
            {
                Name = "transfer",
                Visibility = SolidityVisibilityEnum.External,
                Parameters = new List<FunctionParameterModel> { toParam, amountParam },
                ReturnParameters = new List<ReturnParameterModel> { successParam }
            };
            
            // Add modifier
            transferFunction.Modifiers.Add(whenNotPausedModifier);

            // Create expressions for require statements
            var toExpr = new LiteralExpressionModel("to");
            var zeroAddressExpr = new LiteralExpressionModel("address(0)");
            var notZeroAddressExpr = toExpr.NotEqual(zeroAddressExpr);
            
            var amountExpr = new LiteralExpressionModel("amount");
            var balanceExpr = new LiteralExpressionModel("_balances[msg.sender]");
            var sufficientBalanceExpr = amountExpr.LessOrEqual(balanceExpr);
            
            // Add require statements
            transferFunction.AddStatement(new RequireStatement { 
                Condition = notZeroAddressExpr, 
                Message = "Cannot transfer to zero address" 
            });
            
            transferFunction.AddStatement(new RequireStatement { 
                Condition = sufficientBalanceExpr, 
                Message = "Insufficient balance" 
            });

            // Then statements
            var thenStatements = new StatementModel[]
            {
                new AssignmentStatement {
                    Target = "_balances[msg.sender]",
                    Value = "_balances[msg.sender] - amount"
                },
                new AssignmentStatement {
                    Target = "_balances[to]",
                    Value = "_balances[to] + amount"
                },
                new EmitStatement("Transfer")
                    .AddStringArgument("msg.sender")
                    .AddStringArgument("to")
                    .AddStringArgument("amount"),
                new ReturnStatement("true")
            };
            
            // Else statements
            var elseStatements = new StatementModel[]
            {
                new ReturnStatement("false")
            };
            
            // Create conditional statement
            var condition = ConditionStatementModel.IfElse(
                sufficientBalanceExpr,
                thenStatements,
                elseStatements
            );
            
            // Add the conditional statement to function
            transferFunction.AddStatement(condition);

            return transferFunction;
        }

        // Example of a function with more complex condition (burnFunction)
        private NormalFunctionModel CreateBurnFunction(TypeReference uint256Type, TypeReference boolType, ModifierModel whenNotPausedModifier)
        {
            // Parameters
            var amountParam = new FunctionParameterModel
            {
                Name = "amount",
                Type = uint256Type,
                Index = 0
            };

            // Return parameter
            var successParam = new ReturnParameterModel
            {
                Name = "success",
                Type = boolType,
                Index = 0
            };

            // Create function
            var burnFunction = new NormalFunctionModel
            {
                Name = "burn",
                Visibility = SolidityVisibilityEnum.External,
                Parameters = new List<FunctionParameterModel> { amountParam },
                ReturnParameters = new List<ReturnParameterModel> { successParam }
            };
            
            // Add modifier
            burnFunction.Modifiers.Add(whenNotPausedModifier);

            // Create expressions for require statements
            var amountExpr = new LiteralExpressionModel("amount");
            var zeroExpr = new LiteralExpressionModel("0");
            var balanceExpr = new LiteralExpressionModel("_balances[msg.sender]");
            var totalSupplyExpr = new LiteralExpressionModel("_totalSupply");
            
            var amountGreaterZeroExpr = amountExpr.GreaterThan(zeroExpr);
            var sufficientBalanceExpr = amountExpr.LessOrEqual(balanceExpr);
            var sufficientTotalExpr = amountExpr.LessOrEqual(totalSupplyExpr);
            
            // Add require statements
            burnFunction.AddStatement(new RequireStatement { 
                Condition = amountGreaterZeroExpr, 
                Message = "Zero amount" 
            });
            
            burnFunction.AddStatement(new RequireStatement { 
                Condition = sufficientBalanceExpr, 
                Message = "Insufficient balance" 
            });

            // Create complex if condition - combining two conditions with AND
            var combinedConditionExpr = ExpressionModel.And(
                sufficientBalanceExpr,
                sufficientTotalExpr
            );
            
            // Define statements for then branch
            var thenStatements = new StatementModel[]
            {
                new AssignmentStatement {
                    Target = "_balances[msg.sender]",
                    Value = "_balances[msg.sender] - amount"
                },
                new AssignmentStatement {
                    Target = "_totalSupply",
                    Value = "_totalSupply - amount"
                },
                new EmitStatement("Transfer")
                    .AddStringArgument("msg.sender")
                    .AddStringArgument("address(0)")
                    .AddStringArgument("amount"),
                new ReturnStatement("true")
            };
            
            // Define statements for else branch
            var elseStatements = new StatementModel[]
            {
                new ReturnStatement("false")
            };
            
            // Create the conditional statement using the consolidated model
            var conditionalStatement = ConditionStatementModel.IfElse(
                combinedConditionExpr,
                thenStatements,
                elseStatements
            );
            
            // Add the conditional statement to the function
            burnFunction.AddStatement(conditionalStatement);

            return burnFunction;
        }
 
        // Configuration function that uses a state variable
        private NormalFunctionModel CreateSetFeeFunction(TypeReference uint256Type, ModifierModel onlyOwnerModifier)
        {
            // Parameter
            var newFeeParam = new FunctionParameterModel
            {
                Name = "newFee",
                Type = uint256Type,
                Index = 0
            };

            // Create function
            var setFeeFunction = new NormalFunctionModel
            {
                Name = "setFee",
                Visibility = SolidityVisibilityEnum.External,
                Parameters = new List<FunctionParameterModel> { newFeeParam }
            };
            
            // Add modifier
            setFeeFunction.Modifiers.Add(onlyOwnerModifier);

            // Create expression for require using ExpressionModel
            var newFeeExpr = new LiteralExpressionModel("newFee");
            var maxFeeExpr = new LiteralExpressionModel("1000");
            var validFeeExpr = newFeeExpr.LessOrEqual(maxFeeExpr);
            
            // Add require statement
            setFeeFunction.AddStatement(new RequireStatement { 
                Condition = validFeeExpr, 
                Message = "Fee too high" 
            });

            // Add variable for old fee
            setFeeFunction.AddStatement(new VariableDeclarationStatement(
                uint256Type, 
                "oldFee", 
                "_feeRate", 
                null)
            );
            
            // Update fee
            setFeeFunction.AddStatement(new AssignmentStatement { 
                Target = "_feeRate", 
                Value = "newFee" 
            });
            
            // Emit event
            var emitFeeChanged = new EmitStatement("FeeChanged");
            emitFeeChanged.Arguments.AddRange(new[] { "oldFee", "newFee" });
            setFeeFunction.AddStatement(emitFeeChanged);

            return setFeeFunction;
        }
        
        // Pause function - without parameters
        private NormalFunctionModel CreatePauseFunction(ModifierModel onlyOwnerModifier, ModifierModel whenNotPausedModifier)
        {
            // Create function
            var pauseFunction = new NormalFunctionModel
            {
                Name = "pause",
                Visibility = SolidityVisibilityEnum.External
            };
            
            // Add modifiers
            pauseFunction.Modifiers.Add(onlyOwnerModifier);
            pauseFunction.Modifiers.Add(whenNotPausedModifier);

            // Set paused to true
            pauseFunction.AddStatement(new AssignmentStatement { 
                Target = "_paused", 
                Value = "true" 
            });
            
            // Emit paused event
            var emitPaused = new EmitStatement("Paused");
            pauseFunction.AddStatement(emitPaused);

            return pauseFunction;
        }
        
        // Unpause function - without parameters
        private NormalFunctionModel CreateUnpauseFunction(ModifierModel onlyOwnerModifier, ModifierModel whenPausedModifier)
        {
            // Create function
            var unpauseFunction = new NormalFunctionModel
            {
                Name = "unpause",
                Visibility = SolidityVisibilityEnum.External
            };
            
            // Add modifiers
            unpauseFunction.Modifiers.Add(onlyOwnerModifier);
            unpauseFunction.Modifiers.Add(whenPausedModifier);

            // Set paused to false
            unpauseFunction.AddStatement(new AssignmentStatement { 
                Target = "_paused", 
                Value = "false" 
            });
            
            // Emit unpaused event
            var emitUnpaused = new EmitStatement("Unpaused");
            unpauseFunction.AddStatement(emitUnpaused);

            return unpauseFunction;
        }
        
        // Fee-based transfer function - advanced use of local variables and nested conditions
        private NormalFunctionModel CreateTransferWithFeeFunction(TypeReference addressType, TypeReference uint256Type, TypeReference boolType, ModifierModel whenNotPausedModifier)
        {
            // Parameters
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

            // Return parameter
            var successParam = new ReturnParameterModel
            {
                Name = "success",
                Type = boolType,
                Index = 0
            };

            // Create function
            var transferWithFeeFunction = new NormalFunctionModel
            {
                Name = "transferWithFee",
                Visibility = SolidityVisibilityEnum.External,
                Parameters = new List<FunctionParameterModel> { toParam, amountParam },
                ReturnParameters = new List<ReturnParameterModel> { successParam }
            };
            
            // Add modifier
            transferWithFeeFunction.Modifiers.Add(whenNotPausedModifier);

            // Create expressions using ExpressionModel
            var toExpr = new LiteralExpressionModel("to");
            var zeroAddressExpr = new LiteralExpressionModel("address(0)");
            var notZeroAddressExpr = toExpr.NotEqual(zeroAddressExpr);
            
            var amountExpr = new LiteralExpressionModel("amount");
            var zeroExpr = new LiteralExpressionModel("0");
            var balanceExpr = new LiteralExpressionModel("_balances[msg.sender]");
            var amountGreaterZeroExpr = amountExpr.GreaterThan(zeroExpr);
            var sufficientBalanceExpr = amountExpr.LessOrEqual(balanceExpr);
            
            // Add require statements
            transferWithFeeFunction.AddStatement(new RequireStatement { 
                Condition = notZeroAddressExpr, 
                Message = "Zero address" 
            });
            
            transferWithFeeFunction.AddStatement(new RequireStatement { 
                Condition = amountGreaterZeroExpr, 
                Message = "Zero amount" 
            });
            
            transferWithFeeFunction.AddStatement(new RequireStatement { 
                Condition = sufficientBalanceExpr, 
                Message = "Insufficient balance" 
            });
            
            // Add variables for fee calculation
            transferWithFeeFunction.AddStatement(new VariableDeclarationStatement(
                uint256Type, 
                "feeAmount", 
                "amount * _feeRate / 10000", 
                null)
            );
            
            transferWithFeeFunction.AddStatement(new VariableDeclarationStatement(
                uint256Type, 
                "transferAmount", 
                "amount - feeAmount", 
                null)
            );
            
            // Create condition for fee handling using the consolidated model
            var feeCondition = new ConditionStatementModel();
            
            // Add if block - feeAmount > 0
            var feeAmountExpr = new LiteralExpressionModel("feeAmount");
            var feeConditionExpr = feeAmountExpr.GreaterThan(zeroExpr);
            feeCondition.AddIfBlock(feeConditionExpr);
            
            // Add statements to fee block
            feeCondition.AddStatement(new AssignmentStatement { 
                Target = "_balances[msg.sender]", 
                Value = "_balances[msg.sender] - amount" 
            });
            
            feeCondition.AddStatement(new AssignmentStatement { 
                Target = "_balances[to]", 
                Value = "_balances[to] + transferAmount" 
            });
            
            // Create nested condition for owner fee distribution
            var nestedOwnerCondition = new ConditionStatementModel();
            
            // Add if block - _owner != address(0)
            var ownerExpr = new LiteralExpressionModel("_owner");
            var ownerNotZeroExpr = ownerExpr.NotEqual(zeroAddressExpr);
            nestedOwnerCondition.AddIfBlock(ownerNotZeroExpr);
            
            // Add statements to owner if block
            nestedOwnerCondition.AddStatement(new AssignmentStatement { 
                Target = "_balances[_owner]", 
                Value = "_balances[_owner] + feeAmount" 
            });
            
            var emitToOwner = new EmitStatement("Transfer");
            emitToOwner.Arguments.AddRange(new[] { "msg.sender", "_owner", "feeAmount" });
            nestedOwnerCondition.AddStatement(emitToOwner);
            
            // Add else block to owner condition
            nestedOwnerCondition.AddElseBlock();
            
            // Add statements to owner else block
            nestedOwnerCondition.AddStatement(new AssignmentStatement { 
                Target = "_totalSupply", 
                Value = "_totalSupply - feeAmount" 
            });
            
            var emitToBurn = new EmitStatement("Transfer");
            emitToBurn.Arguments.AddRange(new[] { "msg.sender", "address(0)", "feeAmount" });
            nestedOwnerCondition.AddStatement(emitToBurn);
            
            // Add the owner condition to the fee condition
            feeCondition.AddStatement(nestedOwnerCondition);
            
            // Add transfer event emission to fee condition
            var emitTransfer = new EmitStatement("Transfer");
            emitTransfer.Arguments.AddRange(new[] { "msg.sender", "to", "transferAmount" });
            feeCondition.AddStatement(emitTransfer);
            
            // Add else block to fee condition
            
            // Add statements to fee else block
            feeCondition.AddStatement(new AssignmentStatement { 
                Target = "_balances[msg.sender]", 
                Value = "_balances[msg.sender] - amount" 
            });
            
            feeCondition.AddStatement(new AssignmentStatement { 
                Target = "_balances[to]", 
                Value = "_balances[to] + amount" 
            });
            
            var emitDirectTransfer = new EmitStatement("Transfer");
            emitDirectTransfer.Arguments.AddRange(new[] { "msg.sender", "to", "amount" });
            feeCondition.AddStatement(emitDirectTransfer);
            
            // Add the entire condition structure to function
            transferWithFeeFunction.AddStatement(feeCondition);
            
            // Return success
            transferWithFeeFunction.AddStatement(new ReturnStatement("true"));
            
            return transferWithFeeFunction;
        }
        
        // Function with error handling using custom error
        private NormalFunctionModel CreateSafeTransferFunction(TypeReference addressType, TypeReference uint256Type, TypeReference boolType, ModifierModel whenNotPausedModifier)
        {
            // Parameters
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

            // Return parameter
            var successParam = new ReturnParameterModel
            {
                Name = "success",
                Type = boolType,
                Index = 0
            };

            // Create function with custom error handling
            var safeTransferFunction = new NormalFunctionModel
            {
                Name = "safeTransfer",
                Visibility = SolidityVisibilityEnum.External,
                Parameters = new List<FunctionParameterModel> { toParam, amountParam },
                ReturnParameters = new List<ReturnParameterModel> { successParam },
                CustomError = "TransferFailed" // Enable try-catch generation
            };
            
            // Add modifier
            safeTransferFunction.Modifiers.Add(whenNotPausedModifier);

            // Create expressions for require statements
            var toExpr = new LiteralExpressionModel("to");
            var zeroAddressExpr = new LiteralExpressionModel("address(0)");
            var notZeroAddressExpr = toExpr.NotEqual(zeroAddressExpr);
            
            var amountExpr = new LiteralExpressionModel("amount");
            var zeroExpr = new LiteralExpressionModel("0");
            var amountGreaterZeroExpr = amountExpr.GreaterThan(zeroExpr);
            
            var balanceExpr = new LiteralExpressionModel("_balances[msg.sender]");
            var sufficientBalanceExpr = amountExpr.LessOrEqual(balanceExpr);
            
            // Add require statements
            safeTransferFunction.AddStatement(new RequireStatement { 
                Condition = notZeroAddressExpr, 
                Message = "Zero address" 
            });
            
            safeTransferFunction.AddStatement(new RequireStatement { 
                Condition = amountGreaterZeroExpr, 
                Message = "Zero amount" 
            });
            
            safeTransferFunction.AddStatement(new RequireStatement { 
                Condition = sufficientBalanceExpr, 
                Message = "Insufficient balance" 
            });
            
            // Update balances
            safeTransferFunction.AddStatement(new AssignmentStatement { 
                Target = "_balances[msg.sender]", 
                Value = "_balances[msg.sender] - amount" 
            });
            
            safeTransferFunction.AddStatement(new AssignmentStatement { 
                Target = "_balances[to]", 
                Value = "_balances[to] + amount" 
            });
            
            // Emit transfer event
            var emitTransfer = new EmitStatement("Transfer");
            emitTransfer.Arguments.AddRange(new[] { "msg.sender", "to", "amount" });
            safeTransferFunction.AddStatement(emitTransfer);
            
            // No need to add a return statement as the template will
            // automatically add a "return true" for functions with CustomError
            
            return safeTransferFunction;
        }
        #endregion

        private string GenerateSolidityCode(SolidityFile file)
        {
            var sb = new StringBuilder();
            
            // Render file header
            sb.AppendLine(SolidityTemplateProcessor.FileHeader.Render(file));
            sb.AppendLine();
            
            foreach (var contract in file.Contracts)
            {
                // Render contract header
                sb.Append(SolidityTemplateProcessor.ContractHeader.Render(contract));
                sb.AppendLine(" {");
                sb.AppendLine();
                
                // Render state properties
                foreach (var property in contract.StateProperties)
                {
                    try 
                    {
                        sb.Append("    ");
                        sb.AppendLine(SolidityTemplateProcessor.StateProperties.Render(property));
                    }
                    catch (Exception ex)
                    {
                        sb.AppendLine($"    // Error rendering state property {property.Name}: {ex.Message}");
                    }
                }
                sb.AppendLine();
                
                // Render events
                foreach (var @event in contract.Events)
                {
                    try 
                    {
                        sb.Append("    ");
                        sb.AppendLine(SolidityTemplateProcessor.Events.Render(@event));
                    }
                    catch (Exception ex)
                    {
                        sb.AppendLine($"    // Error rendering event {@event.Name}: {ex.Message}");
                    }
                }
                sb.AppendLine();
                
                // Render errors
                foreach (var error in contract.Errors)
                {
                    try 
                    {
                        sb.Append("    ");
                        sb.AppendLine(SolidityTemplateProcessor.Errors.Render(error));
                    }
                    catch (Exception ex)
                    {
                        sb.AppendLine($"    // Error rendering error {error.Name}: {ex.Message}");
                    }
                }
                sb.AppendLine();
                
                // Render modifiers
                foreach (var modifier in contract.Modifiers)
                {
                    try 
                    {
                        sb.Append("    ");
                        sb.AppendLine(SolidityTemplateProcessor.Modifiers.Render(modifier));
                    }
                    catch (Exception ex)
                    {
                        sb.AppendLine($"    // Error rendering modifier {modifier.Name}: {ex.Message}");
                    }
                }
                sb.AppendLine();
                
                // Render constructor
                try
                {
                    var constructorCode = SolidityTemplateProcessor.Constructor.Render(contract);
                    sb.Append("    ");
                    sb.AppendLine(constructorCode);
                    sb.AppendLine();
                }
                catch (Exception ex)
                {
                    sb.AppendLine($"    // Error rendering constructor: {ex.Message}");
                }
                
                // Render functions
                foreach (var function in contract.Functions)
                {
                    try 
                    {
                        string renderedFunction = SolidityTemplateProcessor.Functions.Render(function);
                        // Indent the function code
                        renderedFunction = "    " + renderedFunction.Replace("\n", "\n    ");
                        sb.AppendLine(renderedFunction);
                        sb.AppendLine();
                    }
                    catch (Exception ex)
                    {
                        string functionName = "";
                        if (function is NormalFunctionModel normalFunction)
                            functionName = normalFunction.Name;
                            
                        sb.AppendLine($"    // Error rendering function {functionName}: {ex.Message}");
                    }
                }
                
                sb.AppendLine("}");
            }
            
            return sb.ToString();
        }
        
        [HttpGet("generate-erc20-with-receive-fallback")]
    public ActionResult GenerateERC20WithReceiveFallback()
    {
        #region Types
        // Define basic types
        var uint256Type = new SimpleTypeReference(SolidityDataTypeEnum.Uint256);
        var uint8Type = new SimpleTypeReference(SolidityDataTypeEnum.Uint8);
        var stringType = new SimpleTypeReference(SolidityDataTypeEnum.String);
        var addressType = new SimpleTypeReference(SolidityDataTypeEnum.Address);
        var boolType = new SimpleTypeReference(SolidityDataTypeEnum.Bool);
        var bytesType = new SimpleTypeReference(SolidityDataTypeEnum.Bytes);

        // Define array types
        var addressArrayType = new ArrayTypeReference(addressType);
        var uint256ArrayType = new ArrayTypeReference(uint256Type);

        // Define mapping types
        var balancesType = new MappingTypeReference(addressType, uint256Type);
        var allowancesType = new MappingTypeReference(addressType, 
                            new MappingTypeReference(addressType, uint256Type));
        #endregion

        #region State Properties
        // Basic token properties
        var nameProperty = new StatePropertyModel 
        { 
            Name = "_name", 
            Type = stringType, 
            Visibility = SolidityVisibilityEnum.Private
        };

        var symbolProperty = new StatePropertyModel 
        { 
            Name = "_symbol", 
            Type = stringType, 
            Visibility = SolidityVisibilityEnum.Private
        };

        var decimalsProperty = new StatePropertyModel 
        { 
            Name = "_decimals", 
            Type = uint8Type, 
            Visibility = SolidityVisibilityEnum.Private,
            IsConstant = true,
            InitialValue = "18"
        };

        var totalSupplyProperty = new StatePropertyModel 
        { 
            Name = "_totalSupply", 
            Type = uint256Type, 
            Visibility = SolidityVisibilityEnum.Private
        };

        var balancesProperty = new StatePropertyModel 
        { 
            Name = "_balances", 
            Type = balancesType, 
            Visibility = SolidityVisibilityEnum.Private
        };

        var allowancesProperty = new StatePropertyModel 
        { 
            Name = "_allowances", 
            Type = allowancesType, 
            Visibility = SolidityVisibilityEnum.Private
        };

        var ownerProperty = new StatePropertyModel 
        { 
            Name = "_owner", 
            Type = addressType, 
            Visibility = SolidityVisibilityEnum.Private
        };

        var treasuryProperty = new StatePropertyModel 
        { 
            Name = "_treasury", 
            Type = addressType, 
            Visibility = SolidityVisibilityEnum.Private
        };

        var feeRateProperty = new StatePropertyModel
        {
            Name = "_feeRate",
            Type = uint256Type,
            Visibility = SolidityVisibilityEnum.Private,
            InitialValue = "0"
        };

        var pausedProperty = new StatePropertyModel
        {
            Name = "_paused",
            Type = boolType,
            Visibility = SolidityVisibilityEnum.Private,
            InitialValue = "false"
        };

        var stateProperties = new List<StatePropertyModel>
        {
            nameProperty,
            symbolProperty,
            decimalsProperty,
            totalSupplyProperty,
            balancesProperty,
            allowancesProperty,
            ownerProperty,
            treasuryProperty,
            feeRateProperty,
            pausedProperty
        };
        #endregion

        #region Events
        // Transfer event
        var fromParam = new EventParameterModel 
        { 
            Name = "from", 
            Type = addressType, 
            IsIndexed = true 
        };
        
        var toParam = new EventParameterModel 
        { 
            Name = "to", 
            Type = addressType, 
            IsIndexed = true 
        };
        
        var valueParam = new EventParameterModel 
        { 
            Name = "value", 
            Type = uint256Type 
        };
        
        var transferEvent = new EventModel 
        { 
            Name = "Transfer", 
            Parameters = new List<EventParameterModel> { fromParam, toParam, valueParam } 
        };

        // Approval event
        var ownerParam = new EventParameterModel 
        { 
            Name = "owner", 
            Type = addressType, 
            IsIndexed = true 
        };
        
        var spenderParam = new EventParameterModel 
        { 
            Name = "spender", 
            Type = addressType, 
            IsIndexed = true 
        };
        
        var approvalEvent = new EventModel 
        { 
            Name = "Approval", 
            Parameters = new List<EventParameterModel> { ownerParam, spenderParam, valueParam } 
        };

        // Fee changed event
        var oldFeeParam = new EventParameterModel
        {
            Name = "oldFee",
            Type = uint256Type
        };

        var newFeeParam = new EventParameterModel
        {
            Name = "newFee",
            Type = uint256Type
        };

        var feeChangedEvent = new EventModel
        {
            Name = "FeeChanged",
            Parameters = new List<EventParameterModel> { oldFeeParam, newFeeParam }
        };

        // Paused events
        var pausedEvent = new EventModel
        {
            Name = "Paused",
            Parameters = new List<EventParameterModel> { }
        };

        var unpausedEvent = new EventModel
        {
            Name = "Unpaused",
            Parameters = new List<EventParameterModel> { }
        };

        var events = new List<EventModel> 
        { 
            transferEvent, 
            approvalEvent, 
            feeChangedEvent,
            pausedEvent,
            unpausedEvent
        };
        #endregion

        #region Errors
        var addressErrorParam = new ErrorParameterModel 
        { 
            Name = "account", 
            Type = addressType 
        };
        
        var zeroAddressError = new ErrorModel 
        { 
            Name = "ZeroAddress", 
            Parameters = new List<ErrorParameterModel> { addressErrorParam } 
        };

        var amountErrorParam = new ErrorParameterModel 
        { 
            Name = "available", 
            Type = uint256Type 
        };
        
        var requiredErrorParam = new ErrorParameterModel 
        { 
            Name = "required", 
            Type = uint256Type 
        };
        
        var insufficientBalanceError = new ErrorModel 
        { 
            Name = "InsufficientBalance", 
            Parameters = new List<ErrorParameterModel> { amountErrorParam, requiredErrorParam } 
        };

        var pausedError = new ErrorModel
        {
            Name = "Paused",
            Parameters = new List<ErrorParameterModel> { }
        };

        var notOwnerError = new ErrorModel
        {
            Name = "NotOwner",
            Parameters = new List<ErrorParameterModel> { }
        };

        var transferFailedError = new ErrorModel
        {
            Name = "TransferFailed",
            Parameters = new List<ErrorParameterModel> { }
        };

        var errors = new List<ErrorModel> 
        { 
            zeroAddressError, 
            insufficientBalanceError,
            pausedError,
            notOwnerError,
            transferFailedError
        };
        #endregion

        #region Modifiers
        var onlyOwnerModifier = new ModifierModel 
        { 
            Name = "onlyOwner", 
            Body = "require(msg.sender == _owner, \"Caller is not the owner\");" 
        };

        var whenNotPausedModifier = new ModifierModel
        {
            Name = "whenNotPaused",
            Body = "require(!_paused, \"Contract is paused\");"
        };

        var whenPausedModifier = new ModifierModel
        {
            Name = "whenPaused",
            Body = "require(_paused, \"Contract is not paused\");"
        };

        var modifiers = new List<ModifierModel> 
        { 
            onlyOwnerModifier, 
            whenNotPausedModifier,
            whenPausedModifier
        };
        #endregion

        #region Constructor
        var nameParameter = new ConstructorParameterModel 
        { 
            Name = "name_", 
            Type = stringType, 
            Index = 0,
            AssignedTo = "_name"
        };
        
        var symbolParameter = new ConstructorParameterModel 
        { 
            Name = "symbol_", 
            Type = stringType, 
            Index = 1,
            AssignedTo = "_symbol"
        };
        
        var initialOwnerParameter = new ConstructorParameterModel 
        { 
            Name = "initialOwner", 
            Type = addressType, 
            Index = 2,
            AssignedTo = "_owner"
        };

        var constructorParameters = new List<ConstructorParameterModel>
        {
            nameParameter,
            symbolParameter,
            initialOwnerParameter
        };
        #endregion

        #region Functions
        // Create ERC20 functions (just basic functions for now, will add fallback/receive later)
        var functions = new List<BaseFunctionModel>();
        
        // Standard ERC20 functions (abbreviated for example)
        var transferFunction = CreateTransferFunction(addressType, uint256Type, boolType, whenNotPausedModifier);
        functions.Add(transferFunction);
        
        var approveFunction = CreateApproveFunction(addressType, uint256Type, boolType, whenNotPausedModifier);
        functions.Add(approveFunction);
        
        var balanceOfFunction = CreateBalanceOfFunction(addressType, uint256Type);
        functions.Add(balanceOfFunction);
        
        var nameFunction = CreateSimpleGetterFunction("name", "_name", stringType);
        functions.Add(nameFunction);
        
        var symbolFunction = CreateSimpleGetterFunction("symbol", "_symbol", stringType);
        functions.Add(symbolFunction);
        #endregion

        #region Contract
        // Create the contract model
        var contract = new SolidityContractModel
        {
            Name = "EnhancedERC20WithFallback",
            StateProperties = stateProperties,
            Events = events,
            Errors = errors,
            Modifiers = modifiers,
            ConstructorParameters = constructorParameters,
            Functions = functions
        };
        #endregion

        #region Add Fallback and Receive Functions
        // Add the fallback and receive functions to the contract
        AddFallbackAndReceiveFunctions(contract);
        #endregion

        #region File Header
        // Create file header
        var minVersion = new SoftwareVersion { Major = 0, Minor = 8, Revision = 20 };
        var maxVersion = new SoftwareVersion { Major = 0, Minor = 8, Revision = 20 };
        var fileHeader = new FileHeaderModel
        {
            License = SpdxLicense.MIT,
            Version = new VersionModel { Minimum = minVersion, Maximum = maxVersion }
        };
        #endregion

        #region File
        // Create the Solidity file
        var file = new SolidityFile
        {
            FileHeader = fileHeader,
            Contracts = new[] { contract }
        };
        #endregion

        #region Render
        // Generate complete Solidity code
        var solidityCode = GenerateSolidityCode(file);
        #endregion

        return Ok(solidityCode);
    }

    private void AddFallbackAndReceiveFunctions(SolidityContractModel contract)
    {
        // Criação de uma função Fallback básica sem parâmetros
        var simpleFallback = new FallbackFunctionModel()
            .MakePayable(); // Torna a função payable
                
        // Adicionar uma lógica de rastreamento de chamadas
        var callCounterIncrement = new AssignmentStatement
        {
            Target = "_fallbackCallCount",
            Value = "_fallbackCallCount + 1"
        };
                    
        simpleFallback.AddStatement(callCounterIncrement);
                
        // Adicionar log de evento
        var fallbackCalledEvent = new EmitStatement("FallbackCalled")
            .AddStringArgument("msg.sender")
            .AddStringArgument("msg.value");
        simpleFallback.AddStatement(fallbackCalledEvent);
                
        // Criação de uma função Fallback mais complexa com parâmetros     
        var advancedFallback = FallbackFunctionModel.CreateWithBytesCalldata();
                
        // Adicionar modificador
        var whenNotPausedModifier = contract.Modifiers.FirstOrDefault(m => m.Name == "whenNotPaused");
        if (whenNotPausedModifier != null)
        {
            advancedFallback.Modifiers.Add(whenNotPausedModifier);
        }
                
        // Adicionar verificações de condição
        var notZeroAddressComparison = ExpressionModel.NotEqual("msg.sender", "address(0)");
        var notContractComparison = ExpressionModel.NotEqual("msg.sender.code.length", "0");
        advancedFallback.WithConditions(new[] { notZeroAddressComparison, notContractComparison }, "Invalid caller");
                
        // Adicionar processamento de dados
        advancedFallback.AddStatement(new VariableDeclarationStatement(
            new SimpleTypeReference(SolidityDataTypeEnum.Bytes),
            "result",
            "abi.encodePacked(msg.sender, block.timestamp, data)",
            SolidityMemoryLocation.Memory
        ));
                
        // Adicionar log de evento
        var fallbackDataEvent = new EmitStatement("FallbackData")
            .AddStringArgument("msg.sender")
            .AddStringArgument("data.length");
        advancedFallback.AddStatement(fallbackDataEvent);
                
        // Adicionar retorno
        advancedFallback.AddStatement(new ReturnStatement("result"));
                
        // Criação de uma função Receive que registra o recebimento de ETH
        var simpleReceive = ReceiveFunctionModel.CreateWithEvent("EtherReceived", "msg.sender", "msg.value");
                
        // Adicionar verificação de valor mínimo
        simpleReceive.WithValueCheck("0.01 ether");
                
        // Adicionar atualização de saldo
        simpleReceive.AddStatement(new AssignmentStatement
        {
            Target = "_etherBalance",
            Value = "_etherBalance + msg.value"
        });
                
        // Criação de uma função Receive para distribuição automática
        var distributionReceive = new ReceiveFunctionModel();
                
        // Adicionar condição de quantidade mínima
        var minimumEthComparison = ExpressionModel.GreaterOrEqual("msg.value", "1 ether");
        var requireStatement = distributionReceive.AddStatement(new RequireStatement
        {
            Condition = minimumEthComparison,
            Message = "Minimum 1 ETH required"
        });
        // Calcular valores para distribuição
        distributionReceive.AddStatement(new VariableDeclarationStatement(
            new SimpleTypeReference(SolidityDataTypeEnum.Uint256),
            "ownerShare",
            "msg.value * 70 / 100",
            null
        ));
                
        distributionReceive.AddStatement(new VariableDeclarationStatement(
            new SimpleTypeReference(SolidityDataTypeEnum.Uint256),
            "treasuryShare",
            "msg.value - ownerShare",
            null
        ));
                
        // Adicionar lógica condicional avançada
        var ownerSet = ExpressionModel.NotEqual("_owner", "address(0)");
        var treasurySet = ExpressionModel.NotEqual("_treasury", "address(0)");
                
        // Criar condições aninhadas
        var distributionCondition = ConditionStatementModel.IfElse(
            ownerSet,
            new StatementModel[] {
                // Se o owner está setado, enviar a parte do owner
                new AssignmentStatement
                {
                    Target = "(bool success,)",
                    Value = "payable(_owner).call{value: ownerShare}(\"\")"
                },
                new RequireStatement
                {
                    Condition = ExpressionModel.Equal("success", "true"),
                    Message = "Owner transfer failed"
                },
                // Verificar se a treasury está setada
                ConditionStatementModel.IfElse(
                    treasurySet,
                    new StatementModel[] {
                        // Enviar para treasury
                        new AssignmentStatement
                        {
                            Target = "(bool treasurySuccess,)",
                            Value = "payable(_treasury).call{value: treasuryShare}(\"\")"
                        },
                                            
                        new RequireStatement
                        {
                            Condition = ExpressionModel.Equal("treasurySuccess", "true"),
                            Message = "Treasury transfer failed"
                        },
                    },
                    new StatementModel[] {
                        // Manter na conta do contrato
                        new AssignmentStatement
                        {
                            Target = "_unclaimedBalance",
                            Value = "_unclaimedBalance + treasuryShare"
                        }
                    }
                )
            },
            new StatementModel[] {
                // Se owner não está setado, manter tudo no contrato
                new AssignmentStatement
                {
                    Target = "_unclaimedBalance",
                    Value = "_unclaimedBalance + msg.value"
                },
            }
        );
                
        distributionReceive.AddStatement(distributionCondition);
                
        // Adicionar as funções ao contrato
        contract.Functions.Add(simpleFallback);
        contract.Functions.Add(advancedFallback);
        contract.Functions.Add(simpleReceive);
        contract.Functions.Add(distributionReceive);
                
        // Adicionar variáveis de estado necessárias
        var uint256Type = new SimpleTypeReference(SolidityDataTypeEnum.Uint256);
        var addressType = new SimpleTypeReference(SolidityDataTypeEnum.Address);
                
        if (!contract.StateProperties.Any(p => p.Name == "_fallbackCallCount"))
        {
            contract.StateProperties.Add(new StatePropertyModel 
            {
                Name = "_fallbackCallCount",
                Type = uint256Type,
                Visibility = SolidityVisibilityEnum.Private,
                InitialValue = "0"
            });
        }
                
        if (!contract.StateProperties.Any(p => p.Name == "_etherBalance"))
        {
            contract.StateProperties.Add(new StatePropertyModel 
            {
                Name = "_etherBalance",
                Type = uint256Type,
                Visibility = SolidityVisibilityEnum.Private,
                InitialValue = "0"
            });
        }
                
        if (!contract.StateProperties.Any(p => p.Name == "_unclaimedBalance"))
        {
            contract.StateProperties.Add(new StatePropertyModel 
            {
                Name = "_unclaimedBalance",
                Type = uint256Type,
                Visibility = SolidityVisibilityEnum.Private,
                InitialValue = "0"
            });
        }
                
        if (!contract.StateProperties.Any(p => p.Name == "_treasury"))
        {
            contract.StateProperties.Add(new StatePropertyModel 
            {
                Name = "_treasury",
                Type = addressType,
                Visibility = SolidityVisibilityEnum.Private
            });
        }
                
        // Adicionar eventos necessários
        bool hasFallbackCalledEvent = contract.Events.Any(e => e.Name == "FallbackCalled");
        if (!hasFallbackCalledEvent)
        {
            var senderParam = new EventParameterModel 
            {
                Name = "sender",
                Type = addressType,
                IsIndexed = true
            };
                    
            var valueParam = new EventParameterModel 
            {
                Name = "value",
                Type = uint256Type
            };
                    
            contract.Events.Add(new EventModel 
            {
                Name = "FallbackCalled",
                Parameters = new List<EventParameterModel> { senderParam, valueParam }
            });
        }
                
        bool hasFallbackDataEvent = contract.Events.Any(e => e.Name == "FallbackData");
        if (!hasFallbackDataEvent)
        {
            var senderParam = new EventParameterModel 
            {
                Name = "sender",
                Type = addressType,
                IsIndexed = true
            };
                    
            var dataLengthParam = new EventParameterModel 
            {
                Name = "dataLength",
                Type = uint256Type
            };
                    
            contract.Events.Add(new EventModel 
            {
                Name = "FallbackData",
                Parameters = new List<EventParameterModel> { senderParam, dataLengthParam }
            });
        }
                
        bool hasEtherReceivedEvent = contract.Events.Any(e => e.Name == "EtherReceived");
        if (!hasEtherReceivedEvent)
        {
            var senderParam = new EventParameterModel 
            {
                Name = "sender",
                Type = addressType,
                IsIndexed = true
            };
                    
            var valueParam = new EventParameterModel 
            {
                Name = "value",
                Type = uint256Type
            };
                    
            contract.Events.Add(new EventModel 
            {
                Name = "EtherReceived",
                Parameters = new List<EventParameterModel> { senderParam, valueParam }
            });
        }
    }
    
        
    }
    
    
}