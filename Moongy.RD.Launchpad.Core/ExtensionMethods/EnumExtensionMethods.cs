using System.Reflection;
using Moongy.RD.Launchpad.Core.Attributes;
using Moongy.RD.Launchpad.Core.Models;

namespace Moongy.RD.Launchpad.Core.ExtensionMethods
{
    public static class EnumExtensionMethods
    {
        public static List<OptionLabelValue> ToOptionLabelValue<TEnum>() where TEnum : Enum
        {
            var result = new List<OptionLabelValue>();

            foreach (var value in Enum.GetValues(typeof(TEnum)))
            {
                var fieldName = value.ToString();
                if (fieldName == null) continue;
                var field = typeof(TEnum).GetField(fieldName);

                var labelAttribute = field?.GetCustomAttribute<OptionLabelAttribute>();

                result.Add(new OptionLabelValue()
                {
                    Label = labelAttribute?.Label ?? value.ToString(),
                    Value = value.ToString()
                });
            }

            return result;
        }
    }
}
