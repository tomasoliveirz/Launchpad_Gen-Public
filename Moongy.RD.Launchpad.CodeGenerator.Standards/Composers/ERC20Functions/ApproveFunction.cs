using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Generator
{
    public class ApproveFunction
    {
        public FunctionDefinition Build()
        {
            var parameters = BuildParameters();
            var returnParameters = BuildReturnParameters();

            #region Identifiers
            var spenderAddress = new ExpressionDefinition { Identifier = "spender" };
            var valueExpr = new ExpressionDefinition { Identifier = "value" };
            var msgSender = new ExpressionDefinition { MemberName = "sender", Target = new ExpressionDefinition { Identifier = "msg" } };
            var trueExpr = new ExpressionDefinition { Identifier = "true" };
            #endregion

            #region Function Calls
            var approveCall = new ExpressionDefinition
            {
                Kind = ExpressionKind.FunctionCall,
                Callee = new ExpressionDefinition { Identifier = "_approve" },
                Arguments = new List<ExpressionDefinition> { msgSender, spenderAddress, valueExpr }
            };

            var approveStatement = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Expression,
                Expression = approveCall
            };
            #endregion

            #region Return Statement
            var returnStatement = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Return,
                Expression = trueExpr
            };
            #endregion

            #region Function Definition
            var res = new FunctionDefinition
            {
                Name = "approve",
                Kind = FunctionKind.Normal,
                Visibility = Visibility.Public,
                Parameters = parameters,
                ReturnParameters = returnParameters,
                Body = new List<FunctionStatementDefinition>
                {
                    approveStatement,
                    returnStatement
                }
            };
            #endregion

            return res;
        }

        private List<ParameterDefinition> BuildParameters()
        {
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
                spender,
                value
            };
            return parameters;
        }

        private List<ParameterDefinition> BuildReturnParameters()
        {
            var returnParam = new ParameterDefinition
            {
                Name = "",
                Type = DataTypeReference.Bool
            };

            return new List<ParameterDefinition> { returnParam };
        }
    }
}