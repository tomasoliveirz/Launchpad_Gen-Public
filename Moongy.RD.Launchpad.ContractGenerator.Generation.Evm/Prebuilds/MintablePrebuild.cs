using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Functions;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Parameters;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements.Expressions;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Modifiers;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Errors;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Events;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.State;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Prebuilds
{
    public static class MintablePrebuild
    {

        // State Variables
        public static StatePropertyModel _ownerStateProperty = new StatePropertyModel
        {
            Name = "_owner",
            Type = DataTypePreBuild.Address,
            Visibility = SolidityVisibilityEnum.Private,
            IsConstant = false,
            IsImmutable = false
        };

        // Errors
        public static ErrorModel OwnableUnauthorizedAccountError = new ErrorModel
        {
            Name = "OwnableUnauthorizedAccount",
            Parameters = new List<ErrorParameterModel>
    {
        new ErrorParameterModel
        {
            Name = "account",
            Type = DataTypePreBuild.Address,
            Index = 0
        }
    }
        };

        public static ErrorModel OwnableInvalidOwnerError = new ErrorModel
        {
            Name = "OwnableInvalidOwner",
            Parameters = new List<ErrorParameterModel>
    {
        new ErrorParameterModel
        {
            Name = "owner",
            Type = DataTypePreBuild.Address,
            Index = 0
        }
    }
        };
        // Constructor

        // Modifiers
        public static ModifierModel onlyOwnerModifier = new ModifierModel
        {
            Name = "onlyOwner",
            Body = "_checkOwner();\n_;"
        };


        // Events
        public static EventModel OwnershipTransferredEvent = new EventModel
        {
            Name = "OwnershipTransferred",
            Parameters = new List<EventParameterModel>
            {
                new EventParameterModel
                {
                    Name = "previousOwner",
                    Type = DataTypePreBuild.Address,
                    IsIndexed = true,
                    Index = 0
                },
                new EventParameterModel
                {
                    Name = "newOwner",
                    Type = DataTypePreBuild.Address,
                    IsIndexed = true,
                    Index = 1
                }
            }
        };


        // Functions
        public static NormalFunctionModel _OwnerFunction = new NormalFunctionModel
        {
            Name = "owner",
            Visibility = SolidityVisibilityEnum.Public,
            Mutability = SolidityFunctionMutabilityEnum.View,
            IsVirtual = true,
            ReturnParameters = new List<ReturnParameterModel>
    {
        new ReturnParameterModel
        {
            Name = "",
            Type = DataTypePreBuild.Address,
            Index = 0
        }
    },
            Statements = new List<StatementModel>
    {
        new ReturnStatement
        {
            ValueExpressions = new List<ExpressionModel>
            {
                new LiteralExpressionModel("_owner")
            }
        }
    }
        };

        public static NormalFunctionModel _CheckOwnerFunction = new NormalFunctionModel
        {
            Name = "_checkOwner",
            Visibility = SolidityVisibilityEnum.Internal,
            IsVirtual = true,
            ReturnParameters = new List<ReturnParameterModel>(),
            Parameters = new List<FunctionParameterModel>(),
            Statements = new List<StatementModel>
    {
        new ConditionStatementModel(
            new BinaryExpressionModel(
                new MethodCallExpressionModel("owner", ""),
                ComparisonOperatorEnum.Different,
                new MethodCallExpressionModel("_msgSender", "")
            )
        ).AddStatement(
            new RevertStatement("OwnableUnauthorizedAccount", "_msgSender()")
        )
    }
        };

        public static NormalFunctionModel _RenounceOwnershipFunction = new NormalFunctionModel
        {
            Name = "renounceOwnership",
            Visibility = SolidityVisibilityEnum.Public,
            IsVirtual = true,
            Modifiers = new List<ModifierModel>
    {
        new ModifierModel
        {
            Name = "onlyOwner",
            Body = ""
        }
    },
            Parameters = new List<FunctionParameterModel>(),
            ReturnParameters = new List<ReturnParameterModel>(),
            Statements = new List<StatementModel>
    {
        new ExpressionStatementModel(
            new MethodCallExpressionModel("_transferOwnership", new LiteralExpressionModel("address(0)"))
        )
    }
        };

        public static NormalFunctionModel TransferOwnershipFunction = new NormalFunctionModel
        {
            Name = "transferOwnership",
            Visibility = SolidityVisibilityEnum.Public,
            IsVirtual = true,
            Modifiers = new List<ModifierModel>
    {
                new ModifierModel
                {
                    Name = "onlyOwner",
                    Body = ""
                }
            },
            Parameters = new List<FunctionParameterModel>
    {
        new FunctionParameterModel
        {
            Name = "newOwner",
            Type = DataTypePreBuild.Address,
            Index = 0
        }
    },
            ReturnParameters = new List<ReturnParameterModel>(),
            Statements = new List<StatementModel>
    {
        new ConditionStatementModel(
            new BinaryExpressionModel(
                new LiteralExpressionModel("newOwner"),

                ComparisonOperatorEnum.Equal,
                new LiteralExpressionModel("address(0)")
            )
        ).AddStatement(
            new RevertStatement("OwnableInvalidOwner", "address(0)")
        ),

        new ExpressionStatementModel(
            new MethodCallExpressionModel("_transferOwnership", new LiteralExpressionModel("newOwner"))
        )
    }
        };

        public static NormalFunctionModel _TransferOwnershipFunction = new NormalFunctionModel
        {
            Name = "_transferOwnership",
            Visibility = SolidityVisibilityEnum.Internal,
            IsVirtual = true,
            Parameters = new List<FunctionParameterModel>
    {
        new FunctionParameterModel
        {
            Name = "newOwner",
            Type = DataTypePreBuild.Address,
            Index = 0
        }
    },
            ReturnParameters = new List<ReturnParameterModel>(),
            Statements = new List<StatementModel>
    {
        new VariableDeclarationStatement(
            DataTypePreBuild.Address,
            "oldOwner",
            new LiteralExpressionModel("_owner")
        ),
        new AssignmentStatement(
            new LiteralExpressionModel("_owner"),
            new LiteralExpressionModel("newOwner")
        ),
        new EmitStatement("OwnershipTransferred")
            .AddExpressionArgument(new LiteralExpressionModel("oldOwner"))
            .AddExpressionArgument(new LiteralExpressionModel("newOwner"))
            }
        };



        }
}
