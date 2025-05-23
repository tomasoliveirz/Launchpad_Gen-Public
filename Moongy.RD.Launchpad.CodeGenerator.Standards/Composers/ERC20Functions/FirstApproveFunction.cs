using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Generator
{
    public class FirstApproveFunction
    {
        public FunctionDefinition Build()
        {
            var parameters = BuildParameters();

            #region Literals
            var ownerAddress = new ExpressionDefinition { Identifier = "owner" };
            var spenderAddress = new ExpressionDefinition { Identifier = "spender" };
            var valueExpr = new ExpressionDefinition { Identifier = "value" };
            var trueExpr = new ExpressionDefinition { Identifier = "true" };
            #endregion

            #region Function Calls
            var approveCall = new ExpressionDefinition
            {
                Kind = ExpressionKind.FunctionCall,
                Callee = new ExpressionDefinition { Identifier = "_approve" },
                Arguments = new List<ExpressionDefinition>
                {
                    ownerAddress,
                    spenderAddress,
                    valueExpr,
                    trueExpr
                }
            };

            var approveStatement = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Expression,
                Expression = approveCall
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
                    approveStatement
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