using System.Linq;
using System.Reflection;
using ScribanSolidityColorizer.Attributes;
using ScribanSolidityColorizer.Enums;

namespace ScribanSolidityColorizer.Expressions
{
    public static class ScribanExpressions
    {

        //–– Condition & Loop keywords
        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.ScribanExpression)]
        public const string If = "if";
        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.ScribanExpression)]
        public const string Else = "else";
        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.ScribanExpression)]
        public const string End = "end";
        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.ScribanExpression)]
        public const string For = "for";
        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.ScribanExpression)]
        public const string In = "in";
        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.ScribanExpression)]
        public const string Is = "is";

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
