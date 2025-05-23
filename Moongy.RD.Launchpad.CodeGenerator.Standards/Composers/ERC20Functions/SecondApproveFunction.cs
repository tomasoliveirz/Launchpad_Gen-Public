using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Helpers;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Generator
{
    public class SecondApproveFunction
    {
        public FunctionDefinition Build()
        {
            var parameters = BuildParameters();


            #region Literals
            var ownerAddress = new ExpressionDefinition { Identifier = "owner" };
            var spenderAddress = new ExpressionDefinition { Identifier = "spender" };
            var valueExpr = new ExpressionDefinition { Identifier = "value" };
            var emitEvent = new ExpressionDefinition { Identifier = "emitEvent" };
            var zeroAddress = new ExpressionDefinition { Identifier = "address(0)" };
            var allowanceExpr = new ExpressionDefinition
            {
                Kind = ExpressionKind.IndexAccess, 
                IndexCollection = new ExpressionDefinition
                {
                    Kind = ExpressionKind.IndexAccess, 
                    IndexCollection = new ExpressionDefinition
                    {
                        Kind = ExpressionKind.Identifier,
                        Identifier = "_allowance"
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
            };
            #endregion

            #region Errors
            var revertParameters = new List<ParameterDefinition>
        {
            new ParameterDefinition
            {
                Name = "address(0)",
                Type = DataTypeReference.Address
            }
        };

            var errorHelperFrom = new IfRevertHelper(
                condition: new ExpressionDefinition
                {
                    Kind = ExpressionKind.Binary,
                    Left = ownerAddress,
                    Operator = BinaryOperator.Equal,
                    Right = zeroAddress
                },
                errorName: "ERC20InvalidOwner",
                revertParameters: revertParameters
            ).Build();

            var errorHelperTo = new IfRevertHelper(
                condition: new ExpressionDefinition
                {
                    Kind = ExpressionKind.Binary,
                    Left = spenderAddress,
                    Operator = BinaryOperator.Equal,
                    Right = zeroAddress
                },
                errorName: "ERC20InvalidSpender",
                revertParameters: revertParameters
            ).Build();


            #endregion

            #region Assignments
            var allowancesAssignment = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Assignment,
                ParameterAssignment = { Left = allowanceExpr, Right = valueExpr },
                Expression = valueExpr
            };
            #endregion

            #region IfStatements

            var approvalEventParams = new List<ParameterDefinition>
            {
                new ParameterDefinition { Name = "owner", Type = DataTypeReference.Address },
                new ParameterDefinition { Name = "spender", Type = DataTypeReference.Address },
                new ParameterDefinition { Name = "value", Type = DataTypeReference.Uint256 }
            };

            var emitApprovalEvent = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Trigger,
                Trigger = new TriggerDefinition
                {
                    Kind = TriggerKind.Log,
                    Name = "Approval",
                    Parameters = approvalEventParams
                }
            };

            var ifEmitEventStatement = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Condition,
                ConditionBranches = new List<ConditionBranch>
                {
                    new ConditionBranch
                    {
                        Condition = emitEvent,
                        Body = new List<FunctionStatementDefinition> { emitApprovalEvent }
                    }
                }
            };
            #endregion


            #region FunctionDefinition

            var res = new FunctionDefinition
            {
                Name = "_approve",
                Kind = FunctionKind.Normal,
                Visibility = Visibility.Internal,
                Parameters = parameters,
                Body = new List<FunctionStatementDefinition>
                {
                    errorHelperFrom,
                    errorHelperTo,
                    allowancesAssignment,
                    ifEmitEventStatement
                }
            };

            #endregion

            return res;
        }

        public List<ParameterDefinition> BuildParameters()
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

            var parameters = new List<ParameterDefinition>
            {
                owner,
                spender,
                value,
                emitEvent
            };
            return parameters;
        }
    }
}