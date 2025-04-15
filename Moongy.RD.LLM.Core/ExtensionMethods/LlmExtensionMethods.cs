using Moongy.RD.LLM.Core.Attributes;
using Moongy.RD.LLM.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.LLM.Core.ExtensionMethods
{
    public static class LlmSchemaExtensions
    {
        /// <summary>
        /// Gets the schema value from an enum decorated with LlmSchemaAttribute.
        /// </summary>
        public static string? Parse<TEnum>(this TEnum value, LlmService? service = null)
            where TEnum : struct, Enum
        {
            var member = typeof(TEnum).GetMember(value.ToString()).FirstOrDefault();
            if (member == null)
                return null;

            var attributes = member.GetCustomAttributes<LlmSchemaAttribute>().ToList();
            if (!attributes.Any())
                return null;

            var selected = service == null
                ? attributes.FirstOrDefault()
                : attributes.FirstOrDefault(x => x.Service == service);

            return selected?.Value;
        }

        /// <summary>
        /// Checks if the enum value is valid for the given service.
        /// </summary>
        public static bool IsValidForService<TEnum>(this TEnum value, LlmService service)
            where TEnum : struct, Enum
        {
            var member = typeof(TEnum).GetMember(value.ToString()).FirstOrDefault();
            if (member == null)
                return false;

            return member.GetCustomAttributes<LlmSchemaAttribute>().Any(x => x.Service == service);
        }
    }

    public static class StringExtensionMethods
    {
        public static double ToDouble(this string str)
        {
            var success = double.TryParse(str, out var value);
            return success ? value : 0;
        }
    }
}
