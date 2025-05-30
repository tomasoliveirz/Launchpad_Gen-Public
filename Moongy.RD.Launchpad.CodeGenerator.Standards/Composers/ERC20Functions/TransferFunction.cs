using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Generator
{
    public class TransferFunction
    {
        public FunctionDefinition Build()
        {
            var parameters = BuildParameters();
            var returnParameters = BuildReturnParameters();

            #region Identifiers
            var toAddress = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "to" };
            var valueExpr = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "value" };
            var msgSender = new ExpressionDefinition { MemberName = "sender", Target = new ExpressionDefinition { Identifier = "msg" } };
            var trueExpr = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "true" };
            #endregion

            #region Function Calls
            var transferCall = new ExpressionDefinition
            {
                Kind = ExpressionKind.FunctionCall,
                Callee = new ExpressionDefinition { Identifier = "_transfer" },
                Arguments = new List<ExpressionDefinition> { msgSender, toAddress, valueExpr }
            };

            var transferStatement = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Expression,
                Expression = transferCall
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
                Name = "transfer",
                Kind = FunctionKind.Normal,
                Visibility = Visibility.Public,
                Parameters = parameters,
                ReturnParameters = returnParameters,
                Body = new List<FunctionStatementDefinition>
                {
                    transferStatement,
                    returnStatement
                }
            };
            #endregion

            return res;
        }

        private List<ParameterDefinition> BuildParameters()
        {
            var to = new ParameterDefinition
            {
                Name = "to",
                Type = DataTypeReference.Address
            };
            var value = new ParameterDefinition
            {
                Name = "value",
                Type = DataTypeReference.Uint256
            };

            var parameters = new List<ParameterDefinition>
            {
                to,
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