using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Generator
{
    public class ERC20Constructor
    {
        public FunctionDefinition Build()
        {
            #region Parameters
            var nameParam = new ParameterDefinition { Name = "name_", Type = DataTypeReference.String };
            var symbolParam = new ParameterDefinition { Name = "symbol_", Type = DataTypeReference.String };
            var parameters = new List<ParameterDefinition> { nameParam, symbolParam };
            #endregion
            
            #region Expressions
            var nameExpr = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "name_" };
            var symbolExpr = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "symbol_" };
            var _name = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "_name" };
            var _symbol = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "_symbol" };
            #endregion
            
            #region Assignments
            var nameAssignment = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Assignment,
                ParameterAssignment = new AssignmentDefinition
                {
                    Left = _name,
                    Right = nameExpr
                }
            };

            var symbolAssignment = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Assignment,
                ParameterAssignment = new AssignmentDefinition
                {
                    Left = _symbol,
                    Right = symbolExpr
                }
            };
            #endregion

            #region Function
            var res = new FunctionDefinition
            {
                Name = "constructor",
                Kind = FunctionKind.Constructor,
                Visibility = Visibility.Public,
                Parameters = parameters,
                Body = new List<FunctionStatementDefinition>
                {
                    nameAssignment,
                    symbolAssignment
                }
            };
            #endregion
            Console.WriteLine("Constructor function built with parameters: " + string.Join(", ", parameters.Select(p => p.Name)));
            return res;
        }
    }
}