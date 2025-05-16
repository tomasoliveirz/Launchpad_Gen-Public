using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Generator
{
    public class ConstructorGenerator
    {
        public FunctionDefinition Build()
        {
            #region Parameters
            // Missing Location ??
            var nameParam = new ParameterDefinition { Name = "name_", Type = DataTypeReference.String };
            var symbolParam = new ParameterDefinition { Name = "symbol_", Type = DataTypeReference.String };
            var parameters = new List<ParameterDefinition> { nameParam, symbolParam };
            #endregion
            
            #region Expressions
            var nameExpr = new ExpressionDefinition { Identifier = "name_" };
            var symbolExpr = new ExpressionDefinition { Identifier = "symbol_" };
            var _name = new ExpressionDefinition { Identifier = "_name" };
            var _symbol = new ExpressionDefinition { Identifier = "_symbol" };
            #endregion
            
            #region Assignments
            var nameAssignment = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Assignment,
                ParameterAssignment = { Left = _name, Right = nameExpr },
                Expression = nameExpr
            };
            #endregion
            
            #region FunctionBody
            var symbolAssignment = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Assignment,
                ParameterAssignment = { Left = _symbol, Right = symbolExpr },
                Expression = symbolExpr
            };
            #endregion

            #region Function
            var res = new FunctionDefinition
            {
                Name = "",
                Parameters = parameters,
                Body = new List<FunctionStatementDefinition>
            {
                nameAssignment,
                symbolAssignment
            }
            };
            #endregion

            return res;
        }
    }

}
