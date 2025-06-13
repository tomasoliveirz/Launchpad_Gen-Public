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
            if (statement == null)
            {
                throw new ArgumentNullException(nameof(statement), "Statement cannot be null");
            }

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
            if (functionStatementDefinition == null)
            {
                throw new ArgumentNullException(nameof(functionStatementDefinition), "Assignment definition cannot be null");
            }
            if (functionStatementDefinition.Left == null)
            {
                throw new ArgumentException("Assignment must have a left operand", nameof(functionStatementDefinition));
            }
            if (functionStatementDefinition.Right == null)
            {
                throw new ArgumentException("Assignment must have a right operand", nameof(functionStatementDefinition));
            }

            try
            {
                var leftExpr = ExpressionSoliditySyntaxHelper.MapExpression(functionStatementDefinition.Left);
                var rightExpr = ExpressionSoliditySyntaxHelper.MapExpression(functionStatementDefinition.Right);
                
                return new AssignmentStatement(leftExpr, rightExpr);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DEBUG MapAssignment failed:");
                Console.WriteLine($"   Left.Kind: {functionStatementDefinition.Left?.Kind}");
                Console.WriteLine($"   Left.Identifier: {functionStatementDefinition.Left?.Identifier ?? "null"}");
                Console.WriteLine($"   Left.Target: {functionStatementDefinition.Left?.Target != null}");
                Console.WriteLine($"   Left.IndexCollection: {functionStatementDefinition.Left?.IndexCollection != null}");
                Console.WriteLine($"   Left.Index: {functionStatementDefinition.Left?.Index != null}");
                Console.WriteLine($"   Right.Kind: {functionStatementDefinition.Right?.Kind}");
                Console.WriteLine($"   Right.Identifier: {functionStatementDefinition.Right?.Identifier ?? "null"}");
                Console.WriteLine($"   Error: {ex.Message}");
                
                throw new Exception($"Failed to map assignment statement: {ex.Message}", ex);
            }
        }

        private static RawStatementModel MapExpression(ExpressionDefinition functionStatementDefinition)
        {
            var exprModel = ExpressionSoliditySyntaxHelper.MapExpression(functionStatementDefinition);

            return new RawStatementModel
            {
                Code = exprModel.ToString() + ";"
            };
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
                    if (statement != null)
                    {
                        var mapped = MapStatement(statement);
                        whileStatement.AddBodyStatement(mapped);
                    }
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
                    if (statement != null)
                    {
                        var mapped = MapStatement(statement);
                        forStatement.AddBodyStatement(mapped);
                    }
                }
            }

            return forStatement;
        }

        private static StatementModel MapTrigger(TriggerDefinition trigger, List<ExpressionDefinition> expressions)
        {
            if (trigger == null)
            {
                throw new ArgumentNullException(nameof(trigger), "Trigger definition cannot be null");
            }

            return trigger.Kind switch
            {
                TriggerKind.Log => MapEmit(trigger, expressions),
                TriggerKind.Error => MapError(trigger, expressions),
                _ => throw new NotSupportedException($"TriggerKind '{trigger.Kind}' is not supported for Solidity mapping")
            };
        }

        private static EmitStatement MapEmit(TriggerDefinition trigger, List<ExpressionDefinition> expressions)
        {
            if (string.IsNullOrEmpty(trigger.Name))
            {
                throw new ArgumentException("Emit trigger must have a name", nameof(trigger));
            }

            var emitStatement = new EmitStatement(trigger.Name);

            if (expressions != null && expressions.Count > 0)
            {
                foreach (var expression in expressions)
                {
                    if (expression != null)
                    {
                        emitStatement.AddExpressionArgument(ExpressionSoliditySyntaxHelper.MapExpression(expression));
                    }
                }
            }
            
            return emitStatement;
        }

        private static RevertStatement MapError(TriggerDefinition trigger, List<ExpressionDefinition> expressions)
        {
            if (string.IsNullOrEmpty(trigger.Name))
            {
                throw new ArgumentException("Error trigger must have a name", nameof(trigger));
            }

            var errorStatement = new RevertStatement();
            errorStatement.Name = trigger.Name;

            if (expressions != null && expressions.Count > 0)
            {
                foreach (var expression in expressions)
                {
                    if (expression != null)
                    {
                        try
                        {
                            var mappedExpr = ExpressionSoliditySyntaxHelper.MapExpression(expression);
                            errorStatement.AddArgument(mappedExpr.ToString());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"    Warning: Failed to map error argument: {ex.Message}");
                        }
                    }
                }
            }
            
            return errorStatement;
        }

        private static ReturnStatement MapReturn(List<ExpressionDefinition> returnValues)
        {
            var returnStatement = new ReturnStatement();
            
            if (returnValues != null && returnValues.Count > 0)
            {
                foreach (var value in returnValues)
                {
                    if (value != null)
                    {
                        try
                        {
                            var mappedValue = ExpressionSoliditySyntaxHelper.MapExpression(value);
                            returnStatement.AddValue(mappedValue);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Warning: Failed to map return value: {ex.Message}");
                            Console.WriteLine($"Expression Kind: {value.Kind}");
                            Console.WriteLine($"Expression Details: Target={value.Target != null}, Index={value.Index != null}, IndexCollection={value.IndexCollection != null}");

                        }
                    }
                }
            }
            
            return returnStatement;
        }
    }
}