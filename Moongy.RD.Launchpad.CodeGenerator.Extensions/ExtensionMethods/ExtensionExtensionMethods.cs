using System.Reflection;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Attributes;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Enums;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.ExtensionMethods;

public static class ExtensionExtensionMethods      
{
    
    // encapsulates the whole reflection logic to find properties decorated with ExtensionPropertyAttribute
    // and returns tuples containing not only the property but also the atribute to facilitate the processment
    public static IEnumerable<(PropertyInfo Property, ExtensionPropertyAttribute? Attribute)> GetExtensionProperties(this object extensionFormSection) 
    {
        return extensionFormSection.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(p => (Property: p, Attribute: p.GetCustomAttribute<ExtensionPropertyAttribute>()))
            .Where(t => t.Attribute != null);
    }

    public static bool IsExtensionActive(this object form, ExtensionEnum extension)
    {
        var attributes = form.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(p => (Property: p, Attribute: p.GetCustomAttribute<ExtensionAttribute>()))
            .Where(t => t.Attribute != null);

        foreach (var attribute in attributes)
        {
            if (attribute.Attribute == null) continue;
            if(attribute.Attribute.Source == extension)
            {
                return true;
            }
        }
        return false;
    }

    public static TReturn? GetExtensionValue<TReturn>(this object form, ExtensionEnum extension)
    {
        var attributes = form.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(p => (Property: p, Attribute: p.GetCustomAttribute<ExtensionAttribute>()))
            .Where(t => t.Attribute != null);

        foreach (var attribute in attributes)
        {
            if (attribute.Attribute == null) continue;
            if (attribute.Attribute.Source == extension)
            {
                var value = attribute.Property.GetValue(form);
                if (value == null) return default;
                return (TReturn)Convert.ChangeType(value, typeof(TReturn)) ?? default;
            }
        }
        return default;
    }

}