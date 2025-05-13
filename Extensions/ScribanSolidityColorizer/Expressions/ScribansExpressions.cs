using System.Linq;
using System.Reflection;
using ScribanSolidityColorizer.Attributes;
using ScribanSolidityColorizer.Enums;

namespace ScribanSolidityColorizer.Expressions
{
    public static class ScribanExpressions
    {

        //–– Condition & Loop keywords
        [ProgrammingLanguageExpression(
            ScribanSolidityTokenTypes.ScribanExpression,
            Description = "Conditional check: executes enclosed block if condition is true.",
            UsageExample = "{{ if condition }} ... {{ end }}")]
        public const string If = "if";

        [ProgrammingLanguageExpression(
            ScribanSolidityTokenTypes.ScribanExpression,
            Description = "Alternative path: runs when previous if/elif was false.",
            UsageExample = "{{ if condition }} ... {{ else }} ... {{ end }}")]
        public const string Else = "else";

        [ProgrammingLanguageExpression(
            ScribanSolidityTokenTypes.ScribanExpression,
            Description = "Marks the end of a control block (if, for, etc.).",
            UsageExample = "{{ if condition }} ... {{ end }}")]
        public const string End = "end";

        [ProgrammingLanguageExpression(
            ScribanSolidityTokenTypes.ScribanExpression,
            Description = "Loop over a sequence or collection.",
            UsageExample = "{{ for item in collection }} ... {{ end }}")]
        public const string For = "for";

        [ProgrammingLanguageExpression(
            ScribanSolidityTokenTypes.ScribanExpression,
            Description = "Used inside loops to define the iteration set.",
            UsageExample = "{{ for item in collection }}")]
        public const string In = "in";

        [ProgrammingLanguageExpression(
            ScribanSolidityTokenTypes.ScribanExpression,
            Description = "Performs a type or value comparison.",
            UsageExample = "{{ if variable is 'value' }} ... {{ end }}")]
        public const string Is = "is";


        [ProgrammingLanguageExpression(
            ScribanSolidityTokenTypes.ScribanOperator,
            Description = "Literal value of false.",
            UsageExample = "")]
        public const string False = "false";

        // Automatic grouped collection
        public static readonly string[] All = GetAllConstants();
        private static string[] GetAllConstants()
        {
            return typeof(ScribanExpressions)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
                .Select(fi => (string)fi.GetRawConstantValue())
                .ToArray();
        }
    }
}
