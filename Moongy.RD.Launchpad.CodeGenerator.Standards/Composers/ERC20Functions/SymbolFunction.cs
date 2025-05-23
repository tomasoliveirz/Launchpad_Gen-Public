using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.ERC20Functions
{
    public class SymbolFunction
    {
        public FunctionDefinition Build()
        {
            var function = new FunctionDefinition
            {
                Name = "symbol",
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
                                LiteralValue = "_symbol",
                            }
                        }
                    }
                }
            };
            return function;
        }
    }
}
