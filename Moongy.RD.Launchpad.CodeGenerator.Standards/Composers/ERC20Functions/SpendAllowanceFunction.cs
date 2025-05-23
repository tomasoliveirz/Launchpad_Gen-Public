using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Helpers;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Generator
{
    public class SpendAllowanceFunction
    {
        public FunctionDefinition Build()
        {
            var parameters = BuildParameters();

            #region Literals
            var ownerAddress = new ExpressionDefinition { Identifier = "owner" };
            var spenderAddress = new ExpressionDefinition { Identifier = "spender" };
            var valueExpr = new ExpressionDefinition { Identifier = "value" };
            var currentAllowanceExpr = new ExpressionDefinition { Identifier = "currentAllowance" };
            var falseExpr = new ExpressionDefinition { Identifier = "false" };
            var typeUint256Max = new ExpressionDefinition { Identifier = "type(uint256).max" };
            #endregion

            #region Variable Declaration
            var allowanceCall = new ExpressionDefinition
            {
                Kind = ExpressionKind.FunctionCall,
                Callee = new ExpressionDefinition { Identifier = "allowance" },
                Arguments = new List<ExpressionDefinition> { ownerAddress, spenderAddress }
            };

            var currentAllowanceDeclaration = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.LocalDeclaration,
                LocalParameter = new ParameterDefinition
                {
                    Name = "currentAllowance",
                    Type = DataTypeReference.Uint256,
                    Value = allowanceCall.ToString()
                }
            };
            #endregion

            #region Inner If Statement

            var revertParameters = new List<ParameterDefinition>
            {
                new ParameterDefinition { Name = "spender", Type = DataTypeReference.Address },
                new ParameterDefinition { Name = "currentAllowance", Type = DataTypeReference.Uint256 },
                new ParameterDefinition { Name = "value", Type = DataTypeReference.Uint256 }
            };

            var errorHelper = new IfRevertHelper(
                  condition: new ExpressionDefinition
                  {
                      Kind = ExpressionKind.Binary,
                      Left = currentAllowanceExpr,
                      Operator = BinaryOperator.LessThan,
                      Right = valueExpr
                  },
                  errorName: "ERC20InvalidSender",
                  revertParameters: revertParameters
              ).Build();
            #endregion

            #region Outer If Statement
            var notMaxCondition = new ExpressionDefinition
            {
                Kind = ExpressionKind.Binary,
                Left = currentAllowanceExpr,
                Operator = BinaryOperator.LessThan,
                Right = typeUint256Max
            };

            var outerIfStatement = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Condition,
                ConditionBranches = new List<ConditionBranch>
                {
                    new ConditionBranch
                    {
                        Condition = notMaxCondition,
                        Body = new List<FunctionStatementDefinition>
                        {
                            errorHelper
                        }
                    }
                }
            };
            #endregion

            #region Function Definition
            var res = new FunctionDefinition
            {
                Name = "_spendAllowance",
                Kind = FunctionKind.Normal,
                Visibility = Visibility.Internal,
                Parameters = parameters,
                Body = new List<FunctionStatementDefinition>
                {
                    currentAllowanceDeclaration,
                    outerIfStatement
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

            var parameters = new List<ParameterDefinition>
            {
                owner,
                spender,
                value
            };
            return parameters;
        }
    }
}