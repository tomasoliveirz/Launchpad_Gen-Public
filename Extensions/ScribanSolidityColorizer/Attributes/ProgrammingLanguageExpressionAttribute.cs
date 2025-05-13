using System;
using ScribanSolidityColorizer.Enums;

namespace ScribanSolidityColorizer.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ProgrammingLanguageExpressionAttribute : Attribute
    {
        public ScribanSolidityTokenTypes Type { get; }
        public string Description { get; set; }
        public string UsageExample { get; set; }
        public ProgrammingLanguageExpressionAttribute(ScribanSolidityTokenTypes type)
        {
            Type = type;
        }
    }
}
