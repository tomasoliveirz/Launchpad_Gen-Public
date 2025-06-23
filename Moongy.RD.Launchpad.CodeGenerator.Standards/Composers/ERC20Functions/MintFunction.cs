using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Helpers;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Generator
{
    public class MintFunction
    {
        public FunctionDefinition Build()
        {
            var parameters = BuildParameters();

            var accountExpr = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "account" };
            var valueExpr = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "value" };
            var zeroAddress = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "address(0)" };
            var totalSupplyExpr = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "_totalSupply" };
            var maxSupplyExpr = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "_max_supply" };

            var body = new List<FunctionStatementDefinition>();

            var invalidReceiverCheck = new FunctionStatementDefinition
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
                                    Name = "ERC20InvalidReceiver"
                                },
                                TriggerArguments = new List<ExpressionDefinition> { zeroAddress }
                            }
                        }
                    }
                }
            };
            body.Add(invalidReceiverCheck);

            //  _max_supply > 0 && totalSupply + value > _max_supply
            var maxSupplyCheck = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Condition,
                ConditionBranches = new List<ConditionBranch>
                {
                    new ConditionBranch
                    {
                        Condition = new ExpressionDefinition
                        {
                            Kind = ExpressionKind.Binary,
                            Left = new ExpressionDefinition
                            {
                                Kind = ExpressionKind.Binary,
                                Left = maxSupplyExpr,
                                Operator = BinaryOperator.GreaterThan,
                                Right = new ExpressionDefinition { Kind = ExpressionKind.Literal, LiteralValue = "0" }
                            },
                            Operator = BinaryOperator.And,
                            Right = new ExpressionDefinition
                            {
                                Kind = ExpressionKind.Binary,
                                Left = new ExpressionDefinition
                                {
                                    Kind = ExpressionKind.Binary,
                                    Left = totalSupplyExpr,
                                    Operator = BinaryOperator.Add,
                                    Right = valueExpr
                                },
                                Operator = BinaryOperator.GreaterThan,
                                Right = maxSupplyExpr
                            }
                        },
                        Body = new List<FunctionStatementDefinition>
                        {
                            new FunctionStatementDefinition
                            {
                                Kind = FunctionStatementKind.Trigger,
                                Trigger = new TriggerDefinition
                                {
                                    Kind = TriggerKind.Error,
                                    Name = "ERC20ExceedsMaxSupply"
                                },
                                TriggerArguments = new List<ExpressionDefinition>
                                {
                                    new ExpressionDefinition
                                    {
                                        Kind = ExpressionKind.Binary,
                                        Left = totalSupplyExpr,
                                        Operator = BinaryOperator.Add,
                                        Right = valueExpr
                                    },
                                    maxSupplyExpr
                                }
                            }
                        }
                    }
                }
            };
            body.Add(maxSupplyCheck);

            // Chama _update(address(0), account, value)
            var updateCall = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Expression,
                Expression = new ExpressionDefinition
                {
                    Kind = ExpressionKind.FunctionCall,
                    Callee = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "_update" },
                    Arguments = new List<ExpressionDefinition> { zeroAddress, accountExpr, valueExpr }
                }
            };
            body.Add(updateCall);

            return new FunctionDefinition
            {
                Name = "_mint",
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