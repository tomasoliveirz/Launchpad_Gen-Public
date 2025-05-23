using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.ERC20Functions
{
    public class DecimalsFunction
    {
        public FunctionDefinition Build()
        {
            var function = new FunctionDefinition
            {
                Name = "decimals",
                Kind = FunctionKind.Normal,
                Visibility = Visibility.Public,
                Body = new List<FunctionStatementDefinition>()
                {
                    new FunctionStatementDefinition
                    {
                        Kind = FunctionStatementKind.Return,
                        ReturnValues = new List<ExpressionDefinition>
                        {
                            new ExpressionDefinition
                            {
                                Kind = ExpressionKind.Literal,
                                LiteralValue = "_decimals",
                            }
                        }
                    }
                },
                ReturnParameters = new List<ParameterDefinition>
                {
                    new ParameterDefinition
                    {
                        Name = "",
                        Type = DataTypeReference.Uint8,
                    }
                },
            };
            return function;
        }
    }
}
