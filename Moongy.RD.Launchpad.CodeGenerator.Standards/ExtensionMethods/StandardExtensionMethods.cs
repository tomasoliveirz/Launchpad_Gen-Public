using System.Reflection;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Attributes;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Enums;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.ExtensionMethods
{
    public static class StandardExtensionMethods
    {
        public static StandardEnum? GetStandardFromForm(this object form)
        {
            var standardAttr = form.GetType().GetCustomAttribute<StandardAttribute>();
            return standardAttr?.Source;
        }

        public static IEnumerable<(PropertyInfo, StandardPropertyAttribute)> GetStandardProperties(this object obj)
        {
            return obj.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(p => (Property: p, Attribute: p.GetCustomAttribute<StandardPropertyAttribute>()))
                .Where(t => t.Attribute != null)!;
        }

        public static IEnumerable<(PropertyInfo, StandardPropertyAttribute)> GetStandardPropertiesRecursive(this Type type, object? instance)
        {
            var result = new List<(PropertyInfo, StandardPropertyAttribute)>();

            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var attr = property.GetCustomAttribute<StandardPropertyAttribute>();
                if (attr != null)
                {
                    result.Add((property, attr));
                }
                else if (!property.PropertyType.IsPrimitive && property.PropertyType != typeof(string))
                {
                    var value = instance != null ? property.GetValue(instance) : null;
                    if (value != null)
                    {
                        result.AddRange(GetStandardPropertiesRecursive(property.PropertyType, value));
                    }
                }
            }

            return result;
        }

        public static StandardEnum? GetStandard(this object form)
        {
            var standardProps = form.GetStandardProperties();
            var distinctStandards = standardProps
                .Select(p => p.Item2.Source)
                .Distinct()
                .ToList();

            if (distinctStandards.Count > 1)
                throw new InvalidOperationException("Multiple standards detected in form properties.");
            return distinctStandards.FirstOrDefault();
        }

        public static bool HasConflictingStandards(this object form, StandardEnum expected)
        {
           var standard = GetStandard(form);
            return standard != expected;
        }

        public static T? GetFormProperty<T>(this object form, StandardEnum standard, string name)
        {
            var result = form.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(p => p.Name == name)?.GetValue(form);
            return result == null ? default: (T)Convert.ChangeType(result, typeof(T)) ?? default;
        }

    }


}
