using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Helpers;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.ERC20Functions
{
    public class _UpdateFunction
    {
        public FunctionDefinition Build()
        {
            
            #region Identifiers
            var from = new ExpressionDefinition
            {
                Kind = ExpressionKind.Identifier,
                Identifier = "from",
            };
            var totalSupply = new ExpressionDefinition
            {
                Kind = ExpressionKind.Identifier,
                Identifier = "_totalSupply",
            };
            var value = new ExpressionDefinition
            {
                Kind = ExpressionKind.Identifier,
                Identifier = "value",
            };
            var zeroAddress = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "address(0)" };
            var to = new ExpressionDefinition
            {
                Kind = ExpressionKind.Identifier,
                Identifier = "to",
            };
            var fromBalance = new ExpressionDefinition
            {
                Kind = ExpressionKind.Identifier,
                Identifier = "fromBalance"
            };
            var balancesTo = new ExpressionDefinition
            {
                Kind = ExpressionKind.IndexAccess,
                IndexCollection = new ExpressionDefinition
                {
                    Kind = ExpressionKind.Identifier,
                    Identifier = "_balances"
                },
                Index = to,
            };
            var balancesFrom = new ExpressionDefinition
            {
                Kind = ExpressionKind.IndexAccess,
                IndexCollection = new ExpressionDefinition
                {
                    Kind = ExpressionKind.Identifier,
                    Identifier = "_balances"
                },
                Index = from,
            };
            #endregion
            #region Parameters
            var toParameter = new ParameterDefinition
            {
                Name = "to",
                Type = DataTypeReference.Address,
            };
            var fromParameter = new ParameterDefinition
            {
                Name = "from",
                Type = DataTypeReference.Address,
            };
            var valueParameter = new ParameterDefinition
            {
                Name = "value",
                Type = DataTypeReference.Uint256,
            };
            var fromBalanceParameter = new ParameterDefinition
            {
                Name = "_balances[from]",
                Type = DataTypeReference.Uint256,
            };
            var parameters = new List<ParameterDefinition>
                {
                    fromParameter,
                    toParameter,
                    valueParameter,
                };
            #endregion

            #region BinaryExpressions
            var totalSupplySum =
                new AssignmentDefinition
                {
                    Left = totalSupply,
                    Right = new ExpressionDefinition
                    {
                        Kind = ExpressionKind.Binary,
                        Operator = BinaryOperator.Add,
                        Left = totalSupply,
                        Right = value
                    }
                };
            var totalSupplySubtraction =
                new AssignmentDefinition
                {
                    Left = totalSupply,
                    Right = new ExpressionDefinition
                    {
                        Kind = ExpressionKind.Binary,
                        Operator = BinaryOperator.Subtract,
                        Left = totalSupply,
                        Right = value
                    }
                };
            #endregion

            #region Triggers
            var insufficientBalanceCondition = new ExpressionDefinition
            {
                Kind = ExpressionKind.Binary,
                Operator = BinaryOperator.LessThan,
                Left = balancesFrom,
                Right = value
            };
            var insufficientBalanceError = new IfRevertHelper(insufficientBalanceCondition, "ERC20InsufficientBalance", new List<ExpressionDefinition> { from, fromBalance, value });

            var emitTransferEvent = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Trigger,
                Trigger = new TriggerDefinition
                {
                    Kind = TriggerKind.Log,
                    Name = "Transfer",
                    Parameters = new List<ParameterDefinition>
                    {
                        fromParameter,
                        toParameter,
                        valueParameter
                    }
                }
            };
            #endregion

            #region IfStatements

            var firstIfStatement =
                new FunctionStatementDefinition
                {
                    Kind = FunctionStatementKind.Condition,
                    ConditionBranches = new List<ConditionBranch>
                    {
                        new ConditionBranch
                        {
                            Condition = new ExpressionDefinition
                            {
                                Kind = ExpressionKind.Binary,
                                Operator = BinaryOperator.Equal,
                                Left = from,
                                Right = zeroAddress
                            },
                            Body = new List<FunctionStatementDefinition>
                            {
                                new FunctionStatementDefinition()
                                {
                                    Kind = FunctionStatementKind.Assignment,
                                    ParameterAssignment = totalSupplySum,
                                }

                            }
                        },
                        new ConditionBranch
                        {
                            Condition = null,
                            Body = new List<FunctionStatementDefinition>
                            {
                                insufficientBalanceError.Build(),
                                new FunctionStatementDefinition()
                                {
                                    Kind = FunctionStatementKind.Assignment,
                                    ParameterAssignment = new AssignmentDefinition
                                    {
                                        Left = new ExpressionDefinition
                                        {
                                            Kind = ExpressionKind.IndexAccess,
                                            IndexCollection = new ExpressionDefinition
                                            {
                                                Kind = ExpressionKind.Identifier,
                                                Identifier = "_balances"
                                            },
                                            Index = from
                                        },
                                        Right = new ExpressionDefinition
                                        {
                                            Kind = ExpressionKind.Binary,
                                            Operator = BinaryOperator.Subtract,
                                            Left = fromBalance,
                                            Right = value
                                        }
                                    },
                                }
                            }
                        }
                    }
                };

            var secondIfStatement =
                new FunctionStatementDefinition
                {
                    Kind = FunctionStatementKind.Condition,
                    ConditionBranches = new List<ConditionBranch>
                    {
                        new ConditionBranch
                        {
                            Condition = new ExpressionDefinition
                            {
                                Kind = ExpressionKind.Binary,
                                Operator = BinaryOperator.Equal,
                                Left = to,
                                Right = zeroAddress
                            },
                            Body = new List<FunctionStatementDefinition>
                            {
                                new FunctionStatementDefinition()
                                {
                                    Kind = FunctionStatementKind.Assignment,
                                    ParameterAssignment = totalSupplySubtraction,
                                }
                            }
                        },
                        new ConditionBranch
                        {
                            Condition = null,
                            Body = new List<FunctionStatementDefinition>
                            {
                                new FunctionStatementDefinition()
                                {
                                    Kind = FunctionStatementKind.Assignment,
                                    ParameterAssignment = new AssignmentDefinition
                                    {
                                        Left = balancesTo,
                                        Right = new ExpressionDefinition
                                        {
                                            Kind = ExpressionKind.Binary,
                                            Operator = BinaryOperator.Add,
                                            Left = balancesTo,
                                            Right = value
                                        }
                                    },
                                }
                            }
                        }
                    }
                };
            #endregion

            var result = new FunctionDefinition
            {
                Name = "_update",
                Visibility = Visibility.Internal,
                Kind = FunctionKind.Normal,
                Parameters = parameters,
                Body = new List<FunctionStatementDefinition>
                {
                    firstIfStatement,
                    secondIfStatement,
                    emitTransferEvent
                },
            };
            return result;
        }
    }
}