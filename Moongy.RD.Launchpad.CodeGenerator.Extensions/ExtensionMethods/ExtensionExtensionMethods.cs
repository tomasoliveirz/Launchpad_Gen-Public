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
    
    // check if a form section is associated witha  specific extension type
    // this is used to determine which extractor should process the form section
    // encapsulates the reflection logic to obtain the ExtensionAttribute atribute
    public static bool HasExtensionSource(this object extensionFormSection, ExtensionEnum source)
    {
        var extensionAttribute = extensionFormSection.GetType()
            .GetCustomAttribute<ExtensionAttribute>();
        return extensionAttribute != null && extensionAttribute.Source == source;
    }

}