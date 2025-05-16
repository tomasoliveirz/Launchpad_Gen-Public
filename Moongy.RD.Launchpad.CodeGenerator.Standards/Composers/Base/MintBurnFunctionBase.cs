using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Helpers;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base
{
    public abstract class MintBurnFunctionBase
    {
        private readonly bool _isMint;

        protected MintBurnFunctionBase(bool isMint)
        {
            _isMint = isMint;
        }
        private List<ParameterDefinition> BuildParameters()
        {
            return new List<ParameterDefinition>
    {
            new ParameterDefinition
            {
                Name = "account",
                Type = DataTypeReference.Address
            },
            new ParameterDefinition
            {
                Name = "value",
                Type = DataTypeReference.Uint256
            }
           };
        }


        public FunctionDefinition Build()
        {
            var parameters = BuildParameters();

            #region Literals
            var accountExpr = new ExpressionDefinition { Identifier = "account" };
            var valueExpr = new ExpressionDefinition { Identifier = "value" };
            var zeroAddress = new ExpressionDefinition { Identifier = "address(0)" };
            #endregion

            #region Errors
            var errorHelper = new IfRevertHelper
            (
                condition: new ExpressionDefinition
                {
                    Kind = ExpressionKind.Binary,
                    Left = accountExpr,
                    Operator = BinaryOperator.Equal,
                    Right = zeroAddress
                },
                errorName: _isMint ? "ERC20InvalidReceiver" : "ERC20InvalidSender",
                errorArguments: new List<ExpressionDefinition> { zeroAddress }
            ).Build();
            #endregion

            #region FunctionCalls
            var updateCall = new ExpressionDefinition
            {
                Kind = ExpressionKind.FunctionCall,
                Callee = new ExpressionDefinition { Identifier = "_update" },
                Arguments = _isMint
                    ? new List<ExpressionDefinition> { zeroAddress, accountExpr, valueExpr }
                    : new List<ExpressionDefinition> { accountExpr, zeroAddress, valueExpr }
            };

            var updateStatement = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Expression,
                Expression = updateCall
            };
            #endregion

            #region Function
            var result = new FunctionDefinition
            {
                Name = _isMint ? "_mint" : "_burn",
                Visibility = Visibility.Internal,
                Parameters = parameters,
                Body = new List<FunctionStatementDefinition>
            {
                errorHelper,
                updateStatement
            }
            };
            #endregion

            return result;
        }


    }
}
