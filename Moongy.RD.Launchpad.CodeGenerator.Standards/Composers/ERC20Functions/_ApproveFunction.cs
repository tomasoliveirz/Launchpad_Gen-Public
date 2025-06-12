using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.ERC20Functions
{
    public class _ApproveFunction
    {
        public FunctionDefinition Build()
        {
            var parameters = BuildParameters();

            #region Validation
            var ownerNotZeroRequire = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Expression,
                Expression = new ExpressionDefinition
                {
                    Kind = ExpressionKind.FunctionCall,
                    Callee = new ExpressionDefinition 
                    { 
                        Kind = ExpressionKind.Identifier,
                        Identifier = "require" 
                    },
                    Arguments = new List<ExpressionDefinition>
                    {
                        new ExpressionDefinition
                        {
                            Kind = ExpressionKind.Binary,
                            Left = new ExpressionDefinition 
                            { 
                                Kind = ExpressionKind.Identifier,
                                Identifier = "owner" 
                            },
                            Operator = BinaryOperator.NotEqual,
                            Right = new ExpressionDefinition
                            {
                                Kind = ExpressionKind.FunctionCall,
                                Callee = new ExpressionDefinition 
                                { 
                                    Kind = ExpressionKind.Identifier,
                                    Identifier = "address" 
                                },
                                Arguments = new List<ExpressionDefinition>
                                {
                                    new ExpressionDefinition 
                                    { 
                                        Kind = ExpressionKind.Literal,
                                        LiteralValue = "0" 
                                    }
                                }
                            }
                        },
                        new ExpressionDefinition 
                        { 
                            Kind = ExpressionKind.Literal,
                            LiteralValue = "\"Approve from zero address\"" 
                        }
                    }
                }
            };

            var spenderNotZeroRequire = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Expression,
                Expression = new ExpressionDefinition
                {
                    Kind = ExpressionKind.FunctionCall,
                    Callee = new ExpressionDefinition 
                    { 
                        Kind = ExpressionKind.Identifier,
                        Identifier = "require" 
                    },
                    Arguments = new List<ExpressionDefinition>
                    {
                        new ExpressionDefinition
                        {
                            Kind = ExpressionKind.Binary,
                            Left = new ExpressionDefinition 
                            { 
                                Kind = ExpressionKind.Identifier,
                                Identifier = "spender" 
                            },
                            Operator = BinaryOperator.NotEqual,
                            Right = new ExpressionDefinition
                            {
                                Kind = ExpressionKind.FunctionCall,
                                Callee = new ExpressionDefinition 
                                { 
                                    Kind = ExpressionKind.Identifier,
                                    Identifier = "address" 
                                },
                                Arguments = new List<ExpressionDefinition>
                                {
                                    new ExpressionDefinition 
                                    { 
                                        Kind = ExpressionKind.Literal,
                                        LiteralValue = "0" 
                                    }
                                }
                            }
                        },
                        new ExpressionDefinition 
                        { 
                            Kind = ExpressionKind.Literal,
                            LiteralValue = "\"Approve to zero address\"" 
                        }
                    }
                }
            };
            #endregion

            #region Set Allowance
            var setAllowance = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Assignment,
                ParameterAssignment = new AssignmentDefinition
                {
                    Left = new ExpressionDefinition
                    {
                        Kind = ExpressionKind.IndexAccess,
                        Target = new ExpressionDefinition
                        {
                            Kind = ExpressionKind.IndexAccess,
                            Target = new ExpressionDefinition 
                            { 
                                Kind = ExpressionKind.Identifier,
                                Identifier = "_allowances" 
                            },
                            Index = new ExpressionDefinition 
                            { 
                                Kind = ExpressionKind.Identifier,
                                Identifier = "owner" 
                            }
                        },
                        Index = new ExpressionDefinition 
                        { 
                            Kind = ExpressionKind.Identifier,
                            Identifier = "spender" 
                        }
                    },
                    Right = new ExpressionDefinition 
                    { 
                        Kind = ExpressionKind.Identifier,
                        Identifier = "value" 
                    }
                }
            };
            #endregion

            #region Approval Event
            var approvalEvent = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Trigger,
                Trigger = new TriggerDefinition
                {
                    Kind = TriggerKind.Log,
                    Name = "Approval",
                    Parameters = new List<ParameterDefinition>
                    {
                        new() { Name = "owner", Type = DataTypeReference.Address },
                        new() { Name = "spender", Type = DataTypeReference.Address },
                        new() { Name = "value", Type = DataTypeReference.Uint256 }
                    }
                },
                TriggerArguments = new List<ExpressionDefinition>
                {
                    new ExpressionDefinition 
                    { 
                        Kind = ExpressionKind.Identifier,
                        Identifier = "owner" 
                    },
                    new ExpressionDefinition 
                    { 
                        Kind = ExpressionKind.Identifier,
                        Identifier = "spender" 
                    },
                    new ExpressionDefinition 
                    { 
                        Kind = ExpressionKind.Identifier,
                        Identifier = "value" 
                    }
                }
            };
            #endregion

            #region Function Definition
            var res = new FunctionDefinition
            {
                Name = "_approve",
                Kind = FunctionKind.Normal,
                Visibility = Visibility.Internal,
                Parameters = parameters,
                Body = new List<FunctionStatementDefinition>
                {
                    ownerNotZeroRequire,
                    spenderNotZeroRequire,
                    setAllowance,
                    approvalEvent
                }
            };
            #endregion

            return res;
        }

        private List<ParameterDefinition> BuildParameters()
        {
            var owner = new ParameterDefinition
            {
                Name = "owner",
                Type = DataTypeReference.Address
            };
            var spender = new ParameterDefinition
            {
                Name = "spender",
                Type = DataTypeReference.Address
            };
            var value = new ParameterDefinition
            {
                Name = "value",
                Type = DataTypeReference.Uint256
            };

            return new List<ParameterDefinition> { owner, spender, value };
        }
    }
}