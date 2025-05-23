using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.ERC20Functions
{
    public class _UpdateFunction
    {
        public FunctionDefinition Build()
        {
            #region Expression
            var from = new ExpressionDefinition
            {
                Kind = ExpressionKind.Identifier,
                Identifier = "from",
            };
            var zeroAddress = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "address(0)" };
            #endregion
            #region Parameters
            var parameters = new List<ParameterDefinition>
                {
                    new ParameterDefinition
                    {
                        Name = "from",
                        Type = DataTypeReference.Address,
                    },
                    new ParameterDefinition
                    {
                        Name = "to",
                        Type = DataTypeReference.Address,
                    },
                    new ParameterDefinition
                    {
                        Name = "value",
                        Type = DataTypeReference.Uint256,
                    },
                };
            #endregion

            #region IfStatements

            var ifStatements =
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

                            }
                        }
                    }
                };
            #endregion

            var result = new FunctionDefinition
            {
                Name = "update",
                Visibility = Visibility.Internal,
                Parameters = parameters,
            };
            return result;
        }
    }
}