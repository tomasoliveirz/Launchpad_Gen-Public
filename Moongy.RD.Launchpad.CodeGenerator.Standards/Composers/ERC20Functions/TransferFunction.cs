using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Helpers;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Generator
{
    public class TransferFunction
    {
        public FunctionDefinition Build()
        {
            var parameters = BuildParameters();


            #region Literals
            var fromAddress = new ExpressionDefinition { Identifier = "from" };
            var toAddress = new ExpressionDefinition { Identifier = "to" };
            var valueExpr = new ExpressionDefinition { Identifier = "value" };
            var zeroAddress = new ExpressionDefinition { Identifier = "address(0)" };
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
                    Left = fromAddress,
                    Operator = BinaryOperator.Equal,
                    Right = zeroAddress
                },
                errorName: "ERC20InvalidSender",
                revertParameters: revertParameters
            ).Build();

            var errorHelperTo = new IfRevertHelper(
                condition: new ExpressionDefinition
                {
                    Kind = ExpressionKind.Binary,
                    Left = toAddress,
                    Operator = BinaryOperator.Equal,
                    Right = zeroAddress
                },
                errorName: "ERC20InvalidReceiver",
                revertParameters: revertParameters
            ).Build();
            #endregion

            #region FunctionCalls
            var updateCall = new ExpressionDefinition
            {
                Kind = ExpressionKind.FunctionCall,
                Callee = new ExpressionDefinition { Identifier = "_update" },
                Arguments = new List<ExpressionDefinition> { fromAddress, toAddress, valueExpr }
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
                Name = "_transfer",
                Kind = FunctionKind.Normal,
                Visibility = Visibility.Internal,
                Parameters = parameters,
            };

            #endregion

            return res;
        }

        public List<ParameterDefinition> BuildParameters()
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
    }
}
