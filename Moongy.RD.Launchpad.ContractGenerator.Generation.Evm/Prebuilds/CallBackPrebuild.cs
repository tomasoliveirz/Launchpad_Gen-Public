using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Errors;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Functions;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Parameters;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements.Expressions;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Prebuilds
{
    public static class CallBackPrebuild
    {
        // Errors
        public static ErrorModel ERC1363TransferFailed = new ErrorModel
        {
            Name = "ERC1363TransferFailed",
            Parameters = new List<ErrorParameterModel>
        {
            new() { Name = "receiver", Type = DataTypePreBuild.Address, Index = 0 },
            new() { Name = "value", Type = DataTypePreBuild.Uint256, Index = 1 }
        }
        };

        public static ErrorModel ERC1363TransferFromFailed = new ErrorModel
        {
            Name = "ERC1363TransferFromFailed",
            Parameters = new List<ErrorParameterModel>
        {
            new() { Name = "sender", Type = DataTypePreBuild.Address, Index = 0 },
            new() { Name = "receiver", Type = DataTypePreBuild.Address, Index = 1 },
            new() { Name = "value", Type = DataTypePreBuild.Uint256, Index = 2 }
        }
        };

        public static ErrorModel ERC1363ApproveFailed = new ErrorModel
        {
            Name = "ERC1363ApproveFailed",
            Parameters = new List<ErrorParameterModel>
        {
            new() { Name = "spender", Type = DataTypePreBuild.Address, Index = 0 },
            new() { Name = "value", Type = DataTypePreBuild.Uint256, Index = 1 }
        }
        };

        // Functions


        public static NormalFunctionModel SupportsInterfaceFunction = new NormalFunctionModel
        {
            Name = "supportsInterface",
            Visibility = SolidityVisibilityEnum.Public,
            IsVirtual = true,
            IsOverride = true,
            Mutability = SolidityFunctionMutabilityEnum.View,
            OverrideSpecifiers = new List<string> { "ERC165", "IERC165" },
            Parameters = new List<FunctionParameterModel>
            {
            new() { Name = "interfaceId", Type = DataTypePreBuild.Bytes4, Index = 0 }
        },
            ReturnParameters = new List<ReturnParameterModel>
        {
                
            new() { Name = "", Type = DataTypePreBuild.Bool, Index = 0 }
        },
           
        };

        public static NormalFunctionModel TransferAndCallFunction = new NormalFunctionModel()
        {
            Name = "transferAndCall",
            Visibility = SolidityVisibilityEnum.Public,
            ReturnParameters = new List<ReturnParameterModel>
    {
        new ReturnParameterModel
        {
            Name = "",
            Type = DataTypePreBuild.Bool,
            Index = 0
        }
    },
            Parameters = new List<FunctionParameterModel>
    {
        new FunctionParameterModel
        {
            Name = "to",
            Type = DataTypePreBuild.Address,
            Index = 0
        },
        new FunctionParameterModel
        {
            Name = "value",
            Type = DataTypePreBuild.Uint256,
            Index = 1
        }
    },
            Statements = new List<StatementModel>
    {
       new ReturnStatement
{
            ValueExpressions = new List<ExpressionModel>
            {
                new MethodCallExpressionModel(
                    "transferAndCall",
                    new LiteralExpressionModel("to"),
                    new LiteralExpressionModel("value"),
                    new LiteralExpressionModel("\"\"")
                )
            }
        }

    }
        };


        public static NormalFunctionModel _TransferAndCallFunction = new NormalFunctionModel()
        {
            Name = "transferAndCall",
            Visibility = SolidityVisibilityEnum.Public,
            IsVirtual = true,
            ReturnParameters = new List<ReturnParameterModel>
    {
        new ReturnParameterModel
        {
            Name = "",
            Type = DataTypePreBuild.Bool,
            Index = 0
        }
    },
            Parameters = new List<FunctionParameterModel>
    {
        new FunctionParameterModel
        {
            Name = "to",
            Type = DataTypePreBuild.Address,
            Index = 0
        },
        new FunctionParameterModel
        {
            Name = "value",
            Type = DataTypePreBuild.Uint256,
            Index = 1
        },
        new FunctionParameterModel
        {
            Name = "data",
            Type = DataTypePreBuild.Bytes,
            Index = 2
        }
    },
            Statements = new List<StatementModel>
    {
        new ConditionStatementModel(
            new UnaryExpressionModel(
                ComparisonOperatorEnum.Not,
                new MethodCallExpressionModel(
                    "transfer",
                    new LiteralExpressionModel("to"),
                    new LiteralExpressionModel("value")
                )
            )
        )
        {
            ConditionalBlocks =
            {
                new ConditionalStatementBlock
                {
                    Condition = new UnaryExpressionModel(
                        ComparisonOperatorEnum.Not,
                        new MethodCallExpressionModel(
                            "transfer",
                            new LiteralExpressionModel("to"),
                            new LiteralExpressionModel("value")
                        )
                    ),
                    IsIfBlock = true,
                    Statements =
                    {
                        new RevertStatement("ERC1363TransferFailed", "to", "value")
                    }
                }
            }
        },

        new ExpressionStatementModel(
            new MethodCallExpressionModel(
                "ERC1363Utils.checkOnERC1363TransferReceived",
                new MethodCallExpressionModel("_msgSender", ""),
                new MethodCallExpressionModel("_msgSender", ""),
                new LiteralExpressionModel("to"),
                new LiteralExpressionModel("value"),
                new LiteralExpressionModel("data")
            )
        ),

        new ReturnStatement
        {
            ValueExpressions = new List<ExpressionModel>
            {
                new LiteralExpressionModel("true")
            }
        }
    }
        };


        public static NormalFunctionModel _TransferFromAndCallFunction = new NormalFunctionModel()
        {
            Name = "transferFromAndCall",
            Visibility = SolidityVisibilityEnum.Public,
            ReturnParameters = new List<ReturnParameterModel>
    {
        new ReturnParameterModel
        {
            Name = "",
            Type = DataTypePreBuild.Bool,
            Index = 0
        }
    },
            Parameters = new List<FunctionParameterModel>
    {
        new FunctionParameterModel
        {
            Name = "from",
            Type = DataTypePreBuild.Address,
            Index = 0
        },
        new FunctionParameterModel
        {
            Name = "to",
            Type = DataTypePreBuild.Address,
            Index = 1
        },
        new FunctionParameterModel
        {
            Name = "value",
            Type = DataTypePreBuild.Uint256,
            Index = 2
        }
    },
            Statements = new List<StatementModel>
    {
        new ReturnStatement
        {
            ValueExpressions = new List<ExpressionModel>
            {
                new MethodCallExpressionModel(
                    "transferFromAndCall",
                    new LiteralExpressionModel("from"),
                    new LiteralExpressionModel("to"),
                    new LiteralExpressionModel("value"),
                    new LiteralExpressionModel("\"\"") 
                )
            }
        }
    }
        };


        public static NormalFunctionModel _TransferFromAndCallWithDataFunction = new NormalFunctionModel()
        {
            Name = "transferFromAndCall",
            Visibility = SolidityVisibilityEnum.Public,
            IsVirtual = true,
            ReturnParameters = new List<ReturnParameterModel>
    {
        new ReturnParameterModel
        {
            Name = "",
            Type = DataTypePreBuild.Bool,
            Index = 0
        }
    },
            Parameters = new List<FunctionParameterModel>
    {
        new FunctionParameterModel
        {
            Name = "from",
            Type = DataTypePreBuild.Address,
            Index = 0
        },
        new FunctionParameterModel
        {
            Name = "to",
            Type = DataTypePreBuild.Address,
            Index = 1
        },
        new FunctionParameterModel
        {
            Name = "value",
            Type = DataTypePreBuild.Uint256,
            Index = 2
        },
        new FunctionParameterModel
        {
            Name = "data",
            Type = DataTypePreBuild.Bytes,
            Index = 3
        }
    },
            Statements = new List<StatementModel>
    {
        new ConditionStatementModel(
            new UnaryExpressionModel(
                ComparisonOperatorEnum.Not,
                new MethodCallExpressionModel(
                    "transferFrom",
                    new LiteralExpressionModel("from"),
                    new LiteralExpressionModel("to"),
                    new LiteralExpressionModel("value")
                )
            )
        )
        .AddStatement(
            new RevertStatement("ERC1363TransferFromFailed", "from", "to", "value")
        ),

        new ExpressionStatementModel(
            new MethodCallExpressionModel(
                "ERC1363Utils.checkOnERC1363TransferReceived",
                new MethodCallExpressionModel("_msgSender", ""),
                new LiteralExpressionModel("from"),
                new LiteralExpressionModel("to"),
                new LiteralExpressionModel("value"),
                new LiteralExpressionModel("data")
            )
        ),

        new ReturnStatement
        {
            ValueExpressions = new List<ExpressionModel>
            {
                new LiteralExpressionModel("true")
            }
        }
    }
        };


        public static NormalFunctionModel _ApproveAndCallFunction = new NormalFunctionModel()
        {
            Name = "approveAndCall",
            Visibility = SolidityVisibilityEnum.Public,
            IsVirtual = false,
            ReturnParameters = new List<ReturnParameterModel>
    {
        new ReturnParameterModel
        {
            Name = "",
            Type = DataTypePreBuild.Bool,
            Index = 0
        }
    },
            Parameters = new List<FunctionParameterModel>
    {
        new FunctionParameterModel
        {
            Name = "spender",
            Type = DataTypePreBuild.Address,
            Index = 0
        },
        new FunctionParameterModel
        {
            Name = "value",
            Type = DataTypePreBuild.Uint256,
            Index = 1
        }
    },
            Statements = new List<StatementModel>
    {
        new ReturnStatement
        {
            ValueExpressions = new List<ExpressionModel>
            {
                new MethodCallExpressionModel(
                    "approveAndCall",
                    new LiteralExpressionModel("spender"),
                    new LiteralExpressionModel("value"),
                    new LiteralExpressionModel("\"\"")
                )
            }
        }
    }
        };

        public static NormalFunctionModel _ApproveAndCallWithDataFunction = new NormalFunctionModel()
        {
            Name = "approveAndCall",
            Visibility = SolidityVisibilityEnum.Public,
            IsVirtual = true,
            ReturnParameters = new List<ReturnParameterModel>
    {
        new ReturnParameterModel
        {
            Name = "",
            Type = DataTypePreBuild.Bool,
            Index = 0
        }
    },
            Parameters = new List<FunctionParameterModel>
    {
        new FunctionParameterModel
        {
            Name = "spender",
            Type = DataTypePreBuild.Address,
            Index = 0
        },
        new FunctionParameterModel
        {
            Name = "value",
            Type = DataTypePreBuild.Uint256,
            Index = 1
        },
        new FunctionParameterModel
        {
            Name = "data",
            Type = DataTypePreBuild.Bytes,
            Index = 2
        }
    },
            Statements = new List<StatementModel>
    {
        new ConditionStatementModel(
            new UnaryExpressionModel(ComparisonOperatorEnum.Not,
                new MethodCallExpressionModel(
                    "approve",
                    new LiteralExpressionModel("spender"),
                    new LiteralExpressionModel("value")
                )
            )
        ).AddStatement(
            new RevertStatement("ERC1363ApproveFailed", "spender", "value")
        ),

        new ExpressionStatementModel(
            new MethodCallExpressionModel(
                "ERC1363Utils.checkOnERC1363ApprovalReceived",
                new MethodCallExpressionModel("_msgSender", ""),
                new LiteralExpressionModel("spender"),
                new LiteralExpressionModel("value"),
                new LiteralExpressionModel("data")
            )
        ),

        new ReturnStatement
        {
            ValueExpressions = new List<ExpressionModel>
            {
                new LiteralExpressionModel("true")
            }
        }
    }
        };

    }
}
