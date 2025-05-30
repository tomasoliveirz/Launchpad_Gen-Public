using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Statements;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Helpers
{
    public class ExpressionSoliditySyntaxHelper
    {
        public static ExpressionModel MapExpression(ExpressionDefinition expression)
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
        private static LiteralExpressionModel MapLiteral(ExpressionDefinition expression)
        {
            return new LiteralExpressionModel(expression.LiteralValue);
        }
        private static IdentifierExpressionModel MapIdentifier(ExpressionDefinition expression)
        {
            return new IdentifierExpressionModel(expression.Identifier);
        }
        private static MethodCallExpressionModel MapFunctionCall(ExpressionDefinition expression)
        {
            var calleeExpression = MapExpression(expression.Callee!);
            string methodName = calleeExpression switch
            {
                LiteralExpressionModel lit => lit.Value ?? throw new Exception("Callee literal has no value"),
                IdentifierExpressionModel id => id.Identifier,
                _ => throw new NotSupportedException("Unsupported callee expression type for method call")
            };

            var mappedArgs = new List<ExpressionModel>();
            if (expression.Arguments != null)
            {
                foreach (var arg in expression.Arguments)
                {
                    mappedArgs.Add(MapExpression(arg));
                }
            }

            return new MethodCallExpressionModel(methodName, mappedArgs.ToArray());
        }

        private static BinaryExpressionModel MapBinaryOperation(ExpressionDefinition expression)
        {
            var left = MapExpression(expression.Left!);
            var right = MapExpression(expression.Right!);
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
            var target = MapExpression(expression.Target!);
            return new MemberAccessExpressionModel(target, expression.MemberName);
        }
        private static IndexAccessExpressionModel MapIndexAccess(ExpressionDefinition expression)
        {
            var target = MapExpression(expression.Target!);
            var index = MapExpression(expression.Index!);
            return new IndexAccessExpressionModel(target, index);
        }
    }
}
