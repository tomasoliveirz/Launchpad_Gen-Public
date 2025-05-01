using System.Linq;
using System.Reflection;
using ScribanSolidityColorizer.Attributes;
using ScribanSolidityColorizer.Enums;

namespace ScribanSolidityColorizer.Expressions
{
    public static class ScribanExpressions
    {
        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.ScribanExpression)]
        public const string TemplateExpressionOpen = "{{";
        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.ScribanExpression)]
        public const string TemplateExpressionClose = "}}";

        //–– Control expressions {% … %}
        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.ScribanControl)]
        public const string TemplateControlOpen = "{%";
        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.ScribanControl)]
        public const string TemplateControlClose = "%}";

        //–– Condition & Loop keywords
        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.ScribanControl)]
        public const string If = "{{if";
        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.ScribanControl)]
        public const string Else = "{{else}}";
        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.ScribanControl)]
        public const string End = "{{end}}";
        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.ScribanControl)]
        public const string For = "for";
        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.ScribanControl)]
        public const string In = "in";
        

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
