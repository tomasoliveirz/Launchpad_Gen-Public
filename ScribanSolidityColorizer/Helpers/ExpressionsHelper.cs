using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ScribanSolidityColorizer.Attributes;
using ScribanSolidityColorizer.Enums;

namespace ScribanSoliditySyntaxHighlighter.Helpers
{
    public static class ExpressionsHelper
    {
        public static Dictionary<ScribanSolidityTokenTypes, string[]> GroupedExpressions(Type type)
        {
            var fields = type
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string));

            var grouped = new Dictionary<ScribanSolidityTokenTypes, List<string>>();

            foreach (var field in fields)
            {
                var attribute = field.GetCustomAttribute<ProgrammingLanguageExpressionAttribute>();
                if (attribute != null)
                {
                    if (!grouped.ContainsKey(attribute.Type))
                        grouped[attribute.Type] = new List<string>();

                    grouped[attribute.Type].Add((string)field.GetRawConstantValue());
                }
            }

            return grouped.ToDictionary(g => g.Key, g => g.Value.ToArray());
        }
    }
}
