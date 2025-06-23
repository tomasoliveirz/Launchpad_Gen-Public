using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Helpers
{
    public static class ConstructorStatementHelper
    {
        public static List<string> GenerateStatements(FunctionDefinition constructor)
        {
            var statements = new List<string>();
            if (constructor?.Body == null) return statements;

            foreach (var statement in constructor.Body)
            {
                var generatedStatement = GenerateStatement(statement, constructor.Parameters);
                if (!string.IsNullOrWhiteSpace(generatedStatement))
                {
                    statements.Add(generatedStatement);
                }
            }

            return statements;
        }

        private static string GenerateStatement(FunctionStatementDefinition statement, List<ParameterDefinition> constructorParameters)
        {
            return statement.Kind switch
            {
                FunctionStatementKind.Assignment when statement.ParameterAssignment != null =>
                    GenerateAssignmentStatement(statement.ParameterAssignment, constructorParameters),
                
                FunctionStatementKind.Expression when statement.Expression?.Kind == ExpressionKind.FunctionCall =>
                    GenerateFunctionCallStatement(statement.Expression),
                
                _ => $"// TODO: Handle statement kind {statement.Kind}"
            };
        }

        private static string GenerateAssignmentStatement(AssignmentDefinition assignment, List<ParameterDefinition> constructorParameters)
        {
            if (IsAutomaticParameterAssignment(assignment, constructorParameters))
            {
                return ""; 
            }

            if (IsOwnerAssignment(assignment))
            {
                return "_owner = msg.sender;";
            }

            if (IsSimpleFieldAssignment(assignment))
            {
                var leftSide = GenerateExpression(assignment.Left!);
                var rightSide = GenerateExpression(assignment.Right!);
                return $"{leftSide} = {rightSide};";
            }

            return $"// TODO: Handle assignment {assignment.Left?.Identifier} = {assignment.Right?.Identifier};";
        }

        private static string GenerateFunctionCallStatement(ExpressionDefinition functionCall)
        {
            var functionName = functionCall.Callee?.Identifier;

            return functionName switch
            {
                "_mint" => GenerateMintCall(functionCall),
                "push" when functionCall.Callee?.Target != null => GeneratePushCall(functionCall),
                _ => GenerateGenericFunctionCall(functionCall)
            };
        }

        private static bool IsAutomaticParameterAssignment(AssignmentDefinition assignment, List<ParameterDefinition> constructorParameters)
        {
            if (assignment.Left?.Kind != ExpressionKind.Identifier || 
                assignment.Right?.Kind != ExpressionKind.Identifier)
                return false;

            var leftField = assignment.Left.Identifier;
            var rightParam = assignment.Right.Identifier;

            if (string.IsNullOrEmpty(leftField) || string.IsNullOrEmpty(rightParam))
                return false;

            var isConstructorParam = constructorParameters?.Any(p => p.Name == rightParam) == true;
            var followsPattern = IsParameterToFieldPattern(rightParam, leftField);

            return isConstructorParam && followsPattern;
        }

        private static bool IsParameterToFieldPattern(string paramName, string fieldName)
        {
            if (paramName.EndsWith("_") && fieldName.StartsWith("_"))
            {
                var paramBase = paramName.TrimEnd('_');
                var fieldBase = fieldName.TrimStart('_');
                return string.Equals(paramBase, fieldBase, StringComparison.OrdinalIgnoreCase);
            }

            if (!paramName.Contains("_") && fieldName.StartsWith("_"))
            {
                var fieldBase = fieldName.TrimStart('_');
                return string.Equals(paramName, fieldBase, StringComparison.OrdinalIgnoreCase);
            }

            return string.Equals(paramName, fieldName, StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsOwnerAssignment(AssignmentDefinition assignment)
        {
            return assignment.Left?.Identifier == "_owner" && 
                   assignment.Right?.Kind == ExpressionKind.MemberAccess &&
                   assignment.Right?.Target?.Identifier == "msg" &&
                   assignment.Right?.MemberName == "sender";
        }

        private static bool IsSimpleFieldAssignment(AssignmentDefinition assignment)
        {
            return assignment.Left?.Kind == ExpressionKind.Identifier &&
                   assignment.Right?.Kind == ExpressionKind.Identifier;
        }

        private static string GenerateMintCall(ExpressionDefinition functionCall)
        {
            if (functionCall.Arguments?.Count >= 2)
            {
                var to = GenerateExpression(functionCall.Arguments[0]);
                var amount = GenerateExpression(functionCall.Arguments[1]);
                return $"_mint({to}, {amount});";
            }
            return "_mint(msg.sender, 0);";
        }

        private static string GeneratePushCall(ExpressionDefinition functionCall)
        {
            if (functionCall.Callee?.Target?.Identifier != null && functionCall.Arguments?.Count > 0)
            {
                var arrayName = functionCall.Callee.Target.Identifier;
                var value = GenerateExpression(functionCall.Arguments[0]);
                return $"{arrayName}.push({value});";
            }
            return "// Invalid push call";
        }

        private static string GenerateGenericFunctionCall(ExpressionDefinition functionCall)
        {
            var functionName = functionCall.Callee?.Identifier ?? "unknownFunction";
            var args = functionCall.Arguments?.Select(GenerateExpression) ?? Enumerable.Empty<string>();
            return $"{functionName}({string.Join(", ", args)});";
        }

        private static string GenerateExpression(ExpressionDefinition expr)
        {
            return expr.Kind switch
            {
                ExpressionKind.Identifier => expr.Identifier ?? "",
                ExpressionKind.Literal => FormatLiteral(expr.LiteralValue),
                ExpressionKind.Binary => GenerateBinaryExpression(expr),
                ExpressionKind.MemberAccess when expr.Target?.Identifier == "msg" && expr.MemberName == "sender" => "msg.sender",
                _ => expr.ToString() ?? ""
            };
        }

        private static string GenerateBinaryExpression(ExpressionDefinition expr)
        {
            if (expr.Left == null || expr.Right == null) return "0";

            var left = GenerateExpression(expr.Left);
            var right = GenerateExpression(expr.Right);
            var op = expr.Operator switch
            {
                BinaryOperator.Multiply => "*",
                BinaryOperator.Divide => "/",
                BinaryOperator.Add => "+",
                BinaryOperator.Subtract => "-",
                BinaryOperator.Power => "**",
                _ => ""
            };

            return $"({left} {op} {right})";
        }

        private static string FormatLiteral(string? literal)
        {
            if (string.IsNullOrEmpty(literal)) return "0";
            
            if (literal.StartsWith("0x") && literal.Length >= 10)
            {
                return literal;
            }
            
            if (decimal.TryParse(literal, out _))
            {
                return literal;
            }
            
            if (literal.StartsWith("\"") && literal.EndsWith("\""))
            {
                return literal;
            }
            
            return $"\"{literal}\"";
        }
    }
}