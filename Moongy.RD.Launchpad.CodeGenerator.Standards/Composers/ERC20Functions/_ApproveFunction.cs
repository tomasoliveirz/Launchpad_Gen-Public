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
            var ownerExpr = new ExpressionDefinition 
            { 
                Kind = ExpressionKind.Identifier,
                Identifier = "owner" 
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
            var emitEventExpr = new ExpressionDefinition 
            { 
                Kind = ExpressionKind.Identifier,
                Identifier = "emitEvent" 
            };
            var zeroAddress = new ExpressionDefinition
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
            };
            #endregion

            #region Validation - Owner Check
            var ownerZeroCondition = new ExpressionDefinition
            {
                Kind = ExpressionKind.Binary,
                Left = ownerExpr,
                Operator = BinaryOperator.Equal,
                Right = zeroAddress
            };

            var ownerRevertStatement = new FunctionStatementDefinition
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
            };

            var ownerValidation = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Condition,
                ConditionBranches = new List<ConditionBranch>
                {
                    new ConditionBranch
                    {
                        Condition = ownerZeroCondition,
                        Body = new List<FunctionStatementDefinition>
                        {
                            ownerRevertStatement
                        }
                    }
                }
            };
            #endregion

            #region Validation - Spender Check
            var spenderZeroCondition = new ExpressionDefinition
            {
                Kind = ExpressionKind.Binary,
                Left = spenderExpr,
                Operator = BinaryOperator.Equal,
                Right = zeroAddress
            };

            var spenderRevertStatement = new FunctionStatementDefinition
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
            };

            var spenderValidation = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Condition,
                ConditionBranches = new List<ConditionBranch>
                {
                    new ConditionBranch
                    {
                        Condition = spenderZeroCondition,
                        Body = new List<FunctionStatementDefinition>
                        {
                            spenderRevertStatement
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
                        IndexCollection = new ExpressionDefinition
                        {
                            Kind = ExpressionKind.IndexAccess,
                            IndexCollection = new ExpressionDefinition 
                            { 
                                Kind = ExpressionKind.Identifier,
                                Identifier = "_allowances" 
                            },
                            Index = ownerExpr
                        },
                        Index = spenderExpr
                    },
                    Right = valueExpr
                }
            };
            #endregion

            #region Conditional Approval Event
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
                    ownerExpr,
                    spenderExpr,
                    valueExpr
                }
            };

            var conditionalEvent = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Condition,
                ConditionBranches = new List<ConditionBranch>
                {
                    new ConditionBranch
                    {
                        Condition = emitEventExpr,
                        Body = new List<FunctionStatementDefinition>
                        {
                            approvalEvent
                        }
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
                    ownerValidation,
                    spenderValidation,
                    setAllowance,
                    conditionalEvent
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
            var emitEvent = new ParameterDefinition
            {
                Name = "emitEvent",
                Type = DataTypeReference.Bool
            };

            return new List<ParameterDefinition> { owner, spender, value, emitEvent };
        }
    }
}