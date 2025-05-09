using Moongy.RD.Launchpad.Core.Attributes;
using Moongy.RD.Launchpad.Core.Enums;
using System.Reflection;

namespace Moongy.RD.Launchpad.Core.ExtensionMethods
{
    public static class TokenTypeExtensionMethods
    {
        public static string GetTokenName(this Type tokenType)
        {
            var attr = tokenType.GetCustomAttribute<TokenAttribute>();
            return attr != null && !string.IsNullOrEmpty(attr.Name)
                ? attr.Name
                : tokenType.Name;
        }

        public static Type[] GetTokensByTag(this Type[] tokens, TokenClassification tag) 
        {
            return [.. tokens.Where(t =>
            {
                var attr = t.GetCustomAttribute<TokenAttribute>();
                return attr != null && attr.Tags != null && attr.Tags.Contains(tag);
            })];
        }

        public static TokenClassification[] GetTokenTags(this Type tokenType)
        {
            var attr = tokenType.GetCustomAttribute<TokenAttribute>();
            return attr?.Tags ?? [];
        }
    }
}
