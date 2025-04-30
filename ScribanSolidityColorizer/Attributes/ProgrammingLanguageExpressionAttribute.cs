using System;
using ScribanSolidityColorizer.Enums;

namespace ScribanSolidityColorizer.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ProgrammingLanguageExpressionAttribute : Attribute
    {
        public ScribanSolidityTokenTypes Type { get; }

        public ProgrammingLanguageExpressionAttribute(ScribanSolidityTokenTypes type)
        {
            Type = type;
        }
    }
}
