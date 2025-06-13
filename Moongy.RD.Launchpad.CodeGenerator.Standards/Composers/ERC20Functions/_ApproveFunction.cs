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

            #region Identifiers
            var ownerAddrExpr = new ExpressionDefinition 
            { 
                Kind = ExpressionKind.Identifier,
                Identifier = "ownerAddr" 
            };
            var spenderExpr = new ExpressionDefinition 
            { 
                Kind = ExpressionKind.Identifier,
                Identifier = "spender" 
            };
            var valueExpr = new ExpressionDefinition 
            { 
                Kind = ExpressionKind.Identifier,
                Identifier = "value" 
            };
            var zeroAddress = new ExpressionDefinition
            {
                Kind = ExpressionKind.Identifier,
                Identifier = "address(0)"
            };
            #endregion

            #region Validation with Custom Errors
            var ownerNotZeroCheck = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Condition,
                ConditionBranches = new List<ConditionBranch>
                {
                    new ConditionBranch
                    {
                        Condition = new ExpressionDefinition
                        {
                            Kind = ExpressionKind.Binary,
                            Left = ownerAddrExpr,
                            Operator = BinaryOperator.Equal,
                            Right = zeroAddress
                        },
                        Body = new List<FunctionStatementDefinition>
                        {
                            new FunctionStatementDefinition
                            {
                                Kind = FunctionStatementKind.Trigger,
                                Trigger = new TriggerDefinition
                                {
                                    Kind = TriggerKind.Error,
                                    Name = "ERC20InvalidApprover"
                                },
                                TriggerArguments = new List<ExpressionDefinition>
                                {
                                    zeroAddress
                                }
                            }
                        }
                    }
                }
            };

            var spenderNotZeroCheck = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Condition,
                ConditionBranches = new List<ConditionBranch>
                {
                    new ConditionBranch
                    {
                        Condition = new ExpressionDefinition
                        {
                            Kind = ExpressionKind.Binary,
                            Left = spenderExpr,
                            Operator = BinaryOperator.Equal,
                            Right = zeroAddress
                        },
                        Body = new List<FunctionStatementDefinition>
                        {
                            new FunctionStatementDefinition
                            {
                                Kind = FunctionStatementKind.Trigger,
                                Trigger = new TriggerDefinition
                                {
                                    Kind = TriggerKind.Error,
                                    Name = "ERC20InvalidSpender"
                                },
                                TriggerArguments = new List<ExpressionDefinition>
                                {
                                    zeroAddress
                                }
                            }
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
                            Index = ownerAddrExpr
                        },
                        Index = spenderExpr
                    },
                    Right = valueExpr
                }
            };
            #endregion

            #region Emit Approval Event
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
                    ownerAddrExpr,
                    spenderExpr,
                    valueExpr
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
                    ownerNotZeroCheck,
                    spenderNotZeroCheck,
                    setAllowance,
                    approvalEvent
                }
            };
            #endregion

            return res;
        }

        private List<ParameterDefinition> BuildParameters()
        {
            var ownerAddr = new ParameterDefinition
            {
                Name = "ownerAddr",
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

            return new List<ParameterDefinition> { ownerAddr, spender, value };
        }
    }
}