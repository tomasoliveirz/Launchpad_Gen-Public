using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Helpers;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Generator
{
    public class BurnFunction
    {
        public FunctionDefinition Build()
        {
            var parameters = BuildParameters();

            #region Identifiers
            var accountExpr = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "account" };
            var valueExpr = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "value" };
            var zeroAddress = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "address(0)" };
            #endregion

            #region Errors
            var revertParameters = new List<ExpressionDefinition>
            {
                new ExpressionDefinition
                {
                    Identifier = "address(0)",
                    Kind = ExpressionKind.Identifier
                }
            };

            var errorHelper = new IfRevertHelper(
                condition: new ExpressionDefinition
                {
                    Kind = ExpressionKind.Binary,
                    Left = accountExpr,
                    Operator = BinaryOperator.Equal,
                    Right = zeroAddress
                },
                errorName: "ERC20InvalidSender",
                revertParameters: revertParameters
            ).Build();
            #endregion

            #region FunctionCalls
            var updateCall = new ExpressionDefinition
            {
                Kind = ExpressionKind.FunctionCall,
                Callee = new ExpressionDefinition { Identifier = "_update" },
                Arguments = new List<ExpressionDefinition> { accountExpr, zeroAddress, valueExpr }
            };

            var updateStatement = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Expression,
                Expression = updateCall
            };
            #endregion

            #region FunctionDefinition
            var res = new FunctionDefinition
            {
                Name = "_burn",
                Kind = FunctionKind.Normal,
                Visibility = Visibility.Internal,
                Parameters = parameters,
                Body = new List<FunctionStatementDefinition>
                {
                    errorHelper,
                    updateStatement
                }
            };
            #endregion

            return res;
        }

        private List<ParameterDefinition> BuildParameters() =>
        [
            new ParameterDefinition { Name = "account", Type = DataTypeReference.Address },
            new ParameterDefinition { Name = "value", Type = DataTypeReference.Uint256 }
        ];
    }
}
