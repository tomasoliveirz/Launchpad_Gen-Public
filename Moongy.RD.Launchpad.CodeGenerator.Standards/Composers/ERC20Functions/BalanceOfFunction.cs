using System.ComponentModel.DataAnnotations;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.ERC20Functions
{
    public class BalanceOfFunction
    {
        public FunctionDefinition Build()
        {
            var function = new FunctionDefinition
            {
                Name = "balanceOf",
                Kind = FunctionKind.Normal,
                Visibility = Visibility.Public,
                Parameters = new List<ParameterDefinition>
                {
                    new ParameterDefinition
                    {
                        Name = "account",
                        Type = DataTypeReference.Address,
                    }
                },
                Body = new List<FunctionStatementDefinition>()
                {
                    new FunctionStatementDefinition
                    {
                        Kind = FunctionStatementKind.Return,
                        ReturnValues = new List<ExpressionDefinition>
                        {
                            new ExpressionDefinition
                            {
                                Kind = ExpressionKind.IndexAccess,
                                Target = new ExpressionDefinition
                                {
                                    Kind = ExpressionKind.Identifier,
                                    Identifier = "_balances"
                                },
                                Index = new ExpressionDefinition
                                {
                                    Kind = ExpressionKind.Identifier,
                                    Identifier = "account"
                                }
                            }
                        }
                    }
                },
                ReturnParameters = new List<ParameterDefinition>
                {
                    new ParameterDefinition
                    {
                        Name = "",
                        Type = DataTypeReference.Uint256,
                    }
                },
            };
            return function;
        }
    }
}