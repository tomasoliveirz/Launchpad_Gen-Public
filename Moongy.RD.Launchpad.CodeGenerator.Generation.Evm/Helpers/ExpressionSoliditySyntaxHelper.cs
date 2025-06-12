using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Statements;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Helpers
{
    public class ExpressionSoliditySyntaxHelper
    {
        public static ExpressionModel MapExpression(ExpressionDefinition expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression), "Expression cannot be null");
            }

            try
            {
                return expression.Kind switch
                {
                    ExpressionKind.Literal => MapLiteral(expression),
                    ExpressionKind.Identifier => MapIdentifier(expression),
                    ExpressionKind.FunctionCall => MapFunctionCall(expression),
                    ExpressionKind.Binary => MapBinaryOperation(expression),
                    ExpressionKind.MemberAccess => MapMemberAccess(expression),
                    ExpressionKind.IndexAccess => MapIndexAccess(expression),
                    _ => throw new NotSupportedException($"ExpressionKind '{expression.Kind}' is not supported for Solidity mapping")
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to map expression of kind '{expression.Kind}': {ex.Message}", ex);
            }
        }

        private static LiteralExpressionModel MapLiteral(ExpressionDefinition expression)
        {
            return new LiteralExpressionModel(expression.LiteralValue);
        }

        private static IdentifierExpressionModel MapIdentifier(ExpressionDefinition expression)
        {
            if (string.IsNullOrEmpty(expression.Identifier))
            {
                throw new ArgumentException("Identifier expression must have a valid identifier", nameof(expression));
            }
            return new IdentifierExpressionModel(expression.Identifier);
        }

        private static MethodCallExpressionModel MapFunctionCall(ExpressionDefinition expression)
        {
            if (expression.Callee == null)
            {
                throw new ArgumentException("Function call expression must have a Callee", nameof(expression));
            }

            var calleeExpression = MapExpression(expression.Callee);
            
            string methodName = calleeExpression switch
            {
                LiteralExpressionModel lit => HandleLiteralCallee(lit, expression.Callee),
                IdentifierExpressionModel id => id.Identifier,
                MemberAccessExpressionModel member => HandleMemberAccessCallee(member),
                _ => throw new NotSupportedException($"Unsupported callee expression type: {calleeExpression.GetType().Name}")
            };

            var mappedArgs = new List<ExpressionModel>();
            if (expression.Arguments != null)
            {
                foreach (var arg in expression.Arguments)
                {
                    if (arg != null)
                    {
                        mappedArgs.Add(MapExpression(arg));
                    }
                }
            }

            return new MethodCallExpressionModel(methodName, mappedArgs.ToArray());
        }

        private static string HandleLiteralCallee(LiteralExpressionModel lit, ExpressionDefinition originalCallee)
        {
            if (!string.IsNullOrEmpty(lit.Value))
            {
                return lit.Value;
            }

            if (!string.IsNullOrEmpty(originalCallee.Identifier))
            {
                return originalCallee.Identifier;
            }

            if (!string.IsNullOrEmpty(originalCallee.MemberName))
            {
                return originalCallee.MemberName;
            }

            throw new Exception($"Callee literal has no value and no fallback available. " +
                              $"LiteralValue='{originalCallee.LiteralValue}', " +
                              $"Identifier='{originalCallee.Identifier}', " +
                              $"MemberName='{originalCallee.MemberName}'");
        }

        private static string HandleMemberAccessCallee(MemberAccessExpressionModel member)
        {
            if (member.Target is IdentifierExpressionModel identifier)
            {
                return $"{identifier.Identifier}.{member.MemberName}";
            }
            
            return member.MemberName ?? throw new Exception("Member access has no member name");
        }

        private static BinaryExpressionModel MapBinaryOperation(ExpressionDefinition expression)
        {
            if (expression.Left == null)
            {
                throw new ArgumentException("Binary operation must have a left operand", nameof(expression));
            }
            if (expression.Right == null)
            {
                throw new ArgumentException("Binary operation must have a right operand", nameof(expression));
            }

            var left = MapExpression(expression.Left);
            var right = MapExpression(expression.Right);
            return new BinaryExpressionModel(left, MapBinaryOperator(expression.Operator), right);
        }

        private static OperatorEnum MapBinaryOperator(BinaryOperator? binaryOperator)
        {
            return binaryOperator switch
            {
                BinaryOperator.Add => OperatorEnum.Add,
                BinaryOperator.Subtract => OperatorEnum.Subtract,
                BinaryOperator.Multiply => OperatorEnum.Multiply,
                BinaryOperator.Divide => OperatorEnum.Divide,
                BinaryOperator.Modulo => OperatorEnum.Modulo,
                BinaryOperator.GreaterThan => OperatorEnum.GreaterThan,
                BinaryOperator.GreaterOrEqualThan => OperatorEnum.GreaterOrEqualTo,
                BinaryOperator.LessOrEqualThan => OperatorEnum.LessThanOrEqualTo,
                BinaryOperator.And => OperatorEnum.And,
                BinaryOperator.Or => OperatorEnum.Or,
                BinaryOperator.Equal => OperatorEnum.Equal,
                BinaryOperator.NotEqual => OperatorEnum.Different,
                BinaryOperator.LessThan => OperatorEnum.LessThan,
                _ => throw new NotSupportedException($"Binary operator '{binaryOperator}' is not supported for Solidity mapping")
            };
        }

        private static MemberAccessExpressionModel MapMemberAccess(ExpressionDefinition expression)
        {
            if (expression.Target == null)
            {
                throw new ArgumentException("Member access expression must have a target", nameof(expression));
            }
            if (string.IsNullOrEmpty(expression.MemberName))
            {
                throw new ArgumentException("Member access expression must have a member name", nameof(expression));
            }

            var target = MapExpression(expression.Target);
            return new MemberAccessExpressionModel(target, expression.MemberName);
        }

        private static IndexAccessExpressionModel MapIndexAccess(ExpressionDefinition expression)
        {
            if (expression.Index == null)
            {
                throw new ArgumentException("Index access expression must have an index", nameof(expression));
            }

            var targetExpression = expression.Target ?? expression.IndexCollection;
            if (targetExpression == null)
            {
                throw new ArgumentException("Index access expression must have a target or index collection", nameof(expression));
            }

            try
            {
                var target = MapExpression(targetExpression);
                var index = MapExpression(expression.Index);
                return new IndexAccessExpressionModel(target, index);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to map index access: {ex.Message}", ex);
            }
        }
    }
}