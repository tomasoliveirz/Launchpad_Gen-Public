using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Statements;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Helpers
{
    public class StatementSoliditySyntaxHelper
    {
        public static StatementModel MapStatement(FunctionStatementDefinition statement)
        {
            return statement.Kind switch
            {
                FunctionStatementKind.LocalDeclaration => MapLocalDeclaration(statement.LocalParameter),
                FunctionStatementKind.Assignment => MapAssignment(statement.ParameterAssignment),
                FunctionStatementKind.Expression => MapExpression(statement.Expression),
                FunctionStatementKind.Condition => MapCondition(statement.ConditionBranches),
                FunctionStatementKind.ConditionLoop => MapConditionLoop(statement.LoopCondition, statement.LoopBody),
                FunctionStatementKind.RangeLoop => MapRangeLoop(statement.RangeStart, statement.RangeEnd, statement.Iterator, statement.LoopBody),
                FunctionStatementKind.Trigger => MapTrigger(statement.Trigger, statement.TriggerArguments),
                FunctionStatementKind.Return => MapReturn(statement.ReturnValues),
                _ => throw new NotSupportedException($"StatementKind '{statement.Kind}' is not supported for Solidity mapping")
            };
        }
        private static AssignmentStatement MapAssignment(AssignmentDefinition functionStatementDefinition)
        {
            return new AssignmentStatement(ExpressionSoliditySyntaxHelper.MapExpression(functionStatementDefinition.Left),
                                           ExpressionSoliditySyntaxHelper.MapExpression(functionStatementDefinition.Right));

        }
        private static RawStatementModel MapExpression(ExpressionDefinition functionStatementDefinition)
        {
            return new RawStatementModel{ Code = ExpressionSoliditySyntaxHelper.MapExpression(functionStatementDefinition).ToString()};
        }
        private static ConditionStatementModel MapCondition(List<ConditionBranch> conditionBranches)
        {
            if (conditionBranches == null || conditionBranches.Count == 0)
                throw new ArgumentException("Condition branches cannot be null or empty.", nameof(conditionBranches));

            ConditionStatementModel conditionStatement = new ConditionStatementModel();

            for (int i = 0; i < conditionBranches.Count; i++)
            {
                var branch = conditionBranches[i];
                var condition = branch.Condition != null
                    ? ExpressionSoliditySyntaxHelper.MapExpression(branch.Condition)
                    : null;

                var statements = branch.Body?.Select(MapStatement).ToList() ?? new List<StatementModel>();

                if (condition == null)
                {
                    if (conditionStatement.ConditionalBlocks.Count == 0)
                        throw new InvalidOperationException("Else block cannot be the first condition branch.");

                    conditionStatement.AddElseBlock(statements.ToArray());
                }
                else
                {
                    if (conditionStatement.ConditionalBlocks.Count == 0)
                    {
                        conditionStatement.AddIfBlock(condition, statements.ToArray());
                    }
                    else
                    {
                        conditionStatement.AddElseIfBlock(condition, statements.ToArray());
                    }
                }
            }

            return conditionStatement;
        }

        private static WhileStatement MapConditionLoop(ExpressionDefinition conditionLoop, List<FunctionStatementDefinition> loopBody)
        {
            if (conditionLoop == null)
                throw new ArgumentNullException(nameof(conditionLoop));

            var conditionExpression = ExpressionSoliditySyntaxHelper.MapExpression(conditionLoop);

            var whileStatement = new WhileStatement(conditionExpression);

            if (loopBody != null)
            {
                foreach (var statement in loopBody)
                {
                    var mapped = MapStatement(statement);
                    whileStatement.AddBodyStatement(mapped);
                }
            }

            return whileStatement;
        }
        private static VariableDeclarationStatement MapLocalDeclaration(ParameterDefinition parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));

            var typeReference = ContextTypeReferenceSyntaxHelper.MapToSolidityTypeReference(parameter.Type);
            
            var variableDeclaration = new VariableDeclarationStatement(typeReference, parameter.Name, parameter.Value);

            return variableDeclaration;
        }


        private static ForStatement MapRangeLoop(
    ExpressionDefinition rangeStart,
    ExpressionDefinition rangeEnd,
    ParameterDefinition iterator,
    List<FunctionStatementDefinition> body)
        {
            if (rangeStart == null) throw new ArgumentNullException(nameof(rangeStart));
            if (rangeEnd == null) throw new ArgumentNullException(nameof(rangeEnd));
            if (iterator == null) throw new ArgumentNullException(nameof(iterator));

            var typeRef = ContextTypeReferenceSyntaxHelper.MapToSolidityTypeReference(iterator.Type);

            var startExpr = ExpressionSoliditySyntaxHelper.MapExpression(rangeStart);

            var init = new VariableDeclarationStatement(
                typeRef,
                iterator.Name,
                startExpr
            );

            var endExpr = ExpressionSoliditySyntaxHelper.MapExpression(rangeEnd);
            var conditionExpr = new BinaryExpressionModel(
                new IdentifierExpressionModel(iterator.Name),
                OperatorEnum.LessThan,
                endExpr
            );

                var incrementExpr = new BinaryExpressionModel(
            new IdentifierExpressionModel(iterator.Name),
            OperatorEnum.Add,
            new LiteralExpressionModel("1")
        );

                var iteratorStatement = new AssignmentStatement(
                    new IdentifierExpressionModel(iterator.Name),
                    incrementExpr
                    );


            var forStatement = new ForStatement(init, conditionExpr.ToString(), iteratorStatement);

            if (body != null)
            {
                foreach (var statement in body)
                {
                    var mapped = MapStatement(statement);
                    forStatement.AddBodyStatement(mapped);
                }
            }

            return forStatement;
        }


        private static StatementModel MapTrigger(TriggerDefinition trigger, List<ExpressionDefinition> expressions)
        {
            return trigger.Kind switch
            {
                TriggerKind.Log => MapEmit(trigger, expressions),
                TriggerKind.Error => MapError(trigger, expressions),
                _ => throw new NotSupportedException($"TriggerKind '{trigger.Kind}' is not supported for Solidity mapping")
            };
        }

        private static EmitStatement MapEmit(TriggerDefinition trigger, List<ExpressionDefinition> expressions)
        {
            if (expressions == null || expressions.Count == 0)
                throw new ArgumentException("Emit must have at least one expression.", nameof(expressions));
            var emitStatement = new EmitStatement(trigger.Name);

            foreach (var expression in expressions)
            {
                emitStatement.AddExpressionArgument(ExpressionSoliditySyntaxHelper.MapExpression(expression));
            }
            return emitStatement;
        }

        private static RevertStatement MapError(TriggerDefinition trigger, List<ExpressionDefinition> expressions)
        {
            if (expressions == null || expressions.Count == 0)
                throw new ArgumentException("Error must have at least one expression.", nameof(expressions));
            var errorStatement = new RevertStatement(trigger.Name);
            foreach (var expression in expressions)
            {
                errorStatement.AddArgument(ExpressionSoliditySyntaxHelper.MapExpression(expression).ToString());
            }
            return errorStatement;
        }

        private static ReturnStatement MapReturn(List<ExpressionDefinition> returnValues)
        {
            if (returnValues == null || returnValues.Count == 0)
                throw new ArgumentException("Return must have at least one value.", nameof(returnValues));
            var returnStatement = new ReturnStatement();
            foreach (var value in returnValues)
            {
                returnStatement.AddValue(ExpressionSoliditySyntaxHelper.MapExpression(value));
            }
            return returnStatement;
        }

    };
    
}