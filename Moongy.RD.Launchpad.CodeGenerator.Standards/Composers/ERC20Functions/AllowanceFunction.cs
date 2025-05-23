using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.ERC20Functions
{
    public class AllowanceFunction
    {
        public FunctionDefinition Build()
        {
            var function = new FunctionDefinition
            {
                Name = "allowance",
                Kind = FunctionKind.Normal,
                Visibility = Visibility.Public,
                Parameters = new List<ParameterDefinition>
            {
                new ParameterDefinition
                {
                    Name = "owner",
                    Type = DataTypeReference.Address,
                },
                new ParameterDefinition
                {
                    Name = "spender",
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
                            IndexCollection = new ExpressionDefinition
                            {
                                Kind = ExpressionKind.IndexAccess, 
                                IndexCollection = new ExpressionDefinition
                                {
                                    Kind = ExpressionKind.Identifier,
                                    Identifier = "_allowances"
                                },
                                Index = new ExpressionDefinition
                                {
                                    Kind = ExpressionKind.Identifier,
                                    Identifier = "owner"
                                }
                            },
                            Index = new ExpressionDefinition
                            {
                                Kind = ExpressionKind.Identifier,
                                Identifier = "spender"
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
