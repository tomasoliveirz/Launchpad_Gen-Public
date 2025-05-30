using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Statements;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Helpers
{
    public class StatementSoliditySyntaxHelper
    {
        public static StatementModel MapStatement(FunctionStatementDefinition statement)
        {
            return statement.Kind switch
            {
                FunctionStatementKind.LocalDeclaration => MapLocalDeclaration(statement),
                FunctionStatementKind.Assignment => MapAssignment(statement.ParameterAssignment),
                FunctionStatementKind.Expression => MapExpression(statement),
                FunctionStatementKind.Condition => MapCondition(statement),
                FunctionStatementKind.ConditionLoop => MapConditionLoop(statement),
                FunctionStatementKind.RangeLoop => MapRangeLoop(statement),
                FunctionStatementKind.Trigger => MapTrigger(statement),
                FunctionStatementKind.Return => MapReturn(statement),
                _ => throw new NotSupportedException($"StatementKind '{statement.Kind}' is not supported for Solidity mapping")
            };
        }
        private static AssignmentStatement MapAssignment(AssignmentDefinition functionStatementDefinition)
        {
            return new AssignmentStatement
            {
                Target = functionStatementDefinition.Left.Identifier,
                Value = functionStatementDefinition.Right.ToString(),
            };
        }
    };
    
}