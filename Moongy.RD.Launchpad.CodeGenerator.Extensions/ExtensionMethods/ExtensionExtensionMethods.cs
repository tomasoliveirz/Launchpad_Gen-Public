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
    
}