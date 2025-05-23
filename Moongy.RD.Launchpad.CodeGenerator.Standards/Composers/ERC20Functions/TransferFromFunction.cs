using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Generator
{
    public class TransferFromFunction
    {
        public FunctionDefinition Build()
        {
            var parameters = BuildParameters();
            var returnParameters = BuildReturnParameters();

            #region Literals
            var fromAddress = new ExpressionDefinition { Identifier = "from" };
            var toAddress = new ExpressionDefinition { Identifier = "to" };
            var valueExpr = new ExpressionDefinition { Identifier = "value" };
            var spenderExpr = new ExpressionDefinition { Identifier = "spender" };
            var trueExpr = new ExpressionDefinition { Identifier = "true" };
            #endregion

            #region Variable Declaration
            var msgSenderCall = new ExpressionDefinition
            {
                Kind = ExpressionKind.FunctionCall,
                Callee = new ExpressionDefinition { Identifier = "_msgSender" },
                Arguments = new List<ExpressionDefinition>()
            };

            var spenderDeclaration = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.LocalDeclaration,
                LocalParameter = new ParameterDefinition
                {
                    Name = "spender",
                    Type = DataTypeReference.Address,
                    //TODO: check this one
                    Value = "_msgSender()"
                }
            };
            #endregion

            #region Function Calls
            var spendAllowanceCall = new ExpressionDefinition
            {
                Kind = ExpressionKind.FunctionCall,
                Callee = new ExpressionDefinition { Identifier = "_spendAllowance" },
                Arguments = new List<ExpressionDefinition> { fromAddress, spenderExpr, valueExpr }
            };

            var spendAllowanceStatement = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Expression,
                Expression = spendAllowanceCall
            };

            var transferCall = new ExpressionDefinition
            {
                Kind = ExpressionKind.FunctionCall,
                Callee = new ExpressionDefinition { Identifier = "_transfer" },
                Arguments = new List<ExpressionDefinition> { fromAddress, toAddress, valueExpr }
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
                Name = "transferFrom",
                Kind = FunctionKind.Normal,
                Visibility = Visibility.Public,
                Parameters = parameters,
                ReturnParameters = returnParameters,
                Body = new List<FunctionStatementDefinition>
                {
                    spenderDeclaration,
                    spendAllowanceStatement,
                    transferStatement,
                    returnStatement
                }
            };
            #endregion

            return res;
        }

        private List<ParameterDefinition> BuildParameters()
        {
            var from = new ParameterDefinition
            {
                Name = "from",
                Type = DataTypeReference.Address
            };
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
                from,
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