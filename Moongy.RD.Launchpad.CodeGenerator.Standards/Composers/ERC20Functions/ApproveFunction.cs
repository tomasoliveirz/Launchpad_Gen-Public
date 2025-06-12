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
            var spenderAddress = new ExpressionDefinition 
            { 
                Kind = ExpressionKind.Identifier,
                Identifier = "spender" 
            };
            var valueExpr = new ExpressionDefinition 
            { 
                Kind = ExpressionKind.Identifier,
                Identifier = "value" 
            };
            var msgSender = new ExpressionDefinition 
            { 
                Kind = ExpressionKind.MemberAccess,
                Target = new ExpressionDefinition 
                { 
                    Kind = ExpressionKind.Identifier,
                    Identifier = "msg" 
                },
                MemberName = "sender"
            };
            var trueExpr = new ExpressionDefinition 
            { 
                Kind = ExpressionKind.Literal, 
                LiteralValue = "true" 
            };
            #endregion

            #region Function Calls
            var approveCall = new ExpressionDefinition
            {
                Kind = ExpressionKind.FunctionCall,
                Callee = new ExpressionDefinition 
                { 
                    Kind = ExpressionKind.Identifier,
                    Identifier = "_approve" 
                },
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
                ReturnValues = new List<ExpressionDefinition> { trueExpr }
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