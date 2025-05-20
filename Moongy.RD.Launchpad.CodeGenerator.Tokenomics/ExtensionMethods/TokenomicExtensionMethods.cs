using System.Reflection;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Attributes;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Enums;

namespace Moongy.RD.Launchpad.CodeGenerator.Tokenomics.ExtensionMethods;

public static class TokenomicExtensionMethods
{
    public static IEnumerable<(PropertyInfo Property, TokenomicPropertyAttribute? Attribute)> GetTokenomicProperties(
        this object tokenomicFormSection)
    {
        return tokenomicFormSection.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(p => (Property: p,
                                    Attribute: p.GetCustomAttribute<TokenomicPropertyAttribute>()))
            .Where(t => t.Attribute != null);
    }
    
    // has tokenomic -- we are probably going to use this on the generator to check if we have a certain tokenomic on the contract
    public static bool HasTokenomicSource(this object tokenomicFormSection, TokenomicEnum source)
    {
        var tokenomicAttribute = tokenomicFormSection.GetType()
            .GetCustomAttribute<TokenomicAttribute>();
        return tokenomicAttribute != null && tokenomicAttribute.Source == source;
    }

}