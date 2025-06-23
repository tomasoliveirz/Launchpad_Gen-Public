using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Generator
{
    public class BurnFunction
    {
        public FunctionDefinition Build()
        {
            var parameters = BuildParameters();

            var accountExpr = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "account" };
            var valueExpr = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "value" };
            var zeroAddress = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "address(0)" };

            var body = new List<FunctionStatementDefinition>();

            // account != address(0)
            var invalidSenderCheck = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Condition,
                ConditionBranches = new List<ConditionBranch>
                {
                    new ConditionBranch
                    {
                        Condition = new ExpressionDefinition
                        {
                            Kind = ExpressionKind.Binary,
                            Left = accountExpr,
                            Operator = BinaryOperator.Equal,
                            Right = zeroAddress
                        },
                        Body = new List<FunctionStatementDefinition>
                        {
                            new FunctionStatementDefinition
                            {
                                Kind = FunctionStatementKind.Trigger,
                                Trigger = new TriggerDefinition
                                {
                                    Kind = TriggerKind.Error,
                                    Name = "ERC20InvalidSender"
                                },
                                TriggerArguments = new List<ExpressionDefinition> { zeroAddress }
                            }
                        }
                    }
                }
            };
            body.Add(invalidSenderCheck);

            // Chama _update(account, address(0), value)
            var updateCall = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Expression,
                Expression = new ExpressionDefinition
                {
                    Kind = ExpressionKind.FunctionCall,
                    Callee = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "_update" },
                    Arguments = new List<ExpressionDefinition> { accountExpr, zeroAddress, valueExpr }
                }
            };
            body.Add(updateCall);

            return new FunctionDefinition
            {
                Name = "_burn",
                Kind = FunctionKind.Normal,
                Visibility = Visibility.Internal,
                Parameters = parameters,
                Body = body
            };
        }

        private List<ParameterDefinition> BuildParameters() =>
        [
            new ParameterDefinition { Name = "account", Type = DataTypeReference.Address },
            new ParameterDefinition { Name = "value", Type = DataTypeReference.Uint256 }
        ];
    }
}