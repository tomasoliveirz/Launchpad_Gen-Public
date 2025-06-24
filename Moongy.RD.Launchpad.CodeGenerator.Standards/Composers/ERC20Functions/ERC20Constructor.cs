using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Generator
{
    public class ERC20Constructor
    {
        public FunctionDefinition Build(ulong premintValue, ulong maxSupplyValue)
        {
            var nameParam = new ParameterDefinition { Name = "name_", Type = DataTypeReference.String };
            var symbolParam = new ParameterDefinition { Name = "symbol_", Type = DataTypeReference.String };
            var parameters = new List<ParameterDefinition> { nameParam, symbolParam };
            
            var body = new List<FunctionStatementDefinition>();

            var nameAssignment = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Assignment,
                ParameterAssignment = new AssignmentDefinition
                {
                    Left = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "_name" },
                    Right = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "name_" }
                }
            };
            body.Add(nameAssignment);

            var symbolAssignment = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Assignment,
                ParameterAssignment = new AssignmentDefinition
                {
                    Left = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "_symbol" },
                    Right = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "symbol_" }
                }
            };
            body.Add(symbolAssignment);
            
            if (maxSupplyValue > 0)
            {
                var maxSupplyAssignment = new FunctionStatementDefinition
                {
                    Kind = FunctionStatementKind.Assignment,
                    ParameterAssignment = new AssignmentDefinition
                    {
                        Left = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "_max_supply" },
                        Right = new ExpressionDefinition
                        {
                            Kind = ExpressionKind.Binary,
                            Operator = BinaryOperator.Multiply,
                            Left = new ExpressionDefinition { Kind = ExpressionKind.Literal, LiteralValue = maxSupplyValue.ToString() },
                            Right = new ExpressionDefinition
                            {
                                Kind = ExpressionKind.Binary,
                                Left = new ExpressionDefinition { Kind = ExpressionKind.Literal, LiteralValue = "10" },
                                Operator = BinaryOperator.Power,
                                Right = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "_decimals" }
                            }
                        }
                    }
                };
                body.Add(maxSupplyAssignment);
            }
            
            if (premintValue > 0)
            {
                var premintCall = new FunctionStatementDefinition
                {
                    Kind = FunctionStatementKind.Expression,
                    Expression = new ExpressionDefinition
                    {
                        Kind = ExpressionKind.FunctionCall,
                        Callee = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "_mint" },
                        Arguments = new List<ExpressionDefinition>
                        {
                            new ExpressionDefinition 
                            { 
                                Kind = ExpressionKind.MemberAccess,
                                Target = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "msg" },
                                MemberName = "sender"
                            },
                            new ExpressionDefinition
                            {
                                Kind = ExpressionKind.Binary,
                                Operator = BinaryOperator.Multiply,
                                Left = new ExpressionDefinition { Kind = ExpressionKind.Literal, LiteralValue = premintValue.ToString() },
                                Right = new ExpressionDefinition 
                                {
                                    Kind = ExpressionKind.Binary, 
                                    Left = new ExpressionDefinition { Kind = ExpressionKind.Literal, LiteralValue = "10"},
                                    Operator = BinaryOperator.Power,
                                    Right = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "_decimals" }
                                } 
                            }
                        }
                    }
                };
                body.Add(premintCall);
            }

            return new FunctionDefinition
            {
                Name = "constructor",
                Kind = FunctionKind.Constructor,
                Visibility = Visibility.Public,
                Parameters = parameters,
                Body = body
            };
        }
    }
}