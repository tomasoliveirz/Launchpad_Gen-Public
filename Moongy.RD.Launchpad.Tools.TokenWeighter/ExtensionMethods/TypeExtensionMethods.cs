using Moongy.RD.Launchpad.Generator.Extensions.Core.Attributes;
using Moongy.RD.Launchpad.Generator.Extensions.Core.Interfaces;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Attributes;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;

namespace Moongy.RD.Launchpad.Tools.TokenWeighter.ExtensionMethods;

public static class TypeExtensionMethods
{
    public static bool IsWeightable(this Type? t)
    {
        if (t == null) return false;
        bool isTokenomic = t.GetCustomAttributes(inherit: false)
                             .OfType<TokenomicAttribute>()
                             .Any()
                          && typeof(ITokenomic).IsAssignableFrom(t);

        if (isTokenomic)
            return true;

        bool isContractExt = t.GetCustomAttributes(inherit: false)
                              .OfType<ContractExtensionAttribute>()
                              .Any()
                           || typeof(IContractExtension).IsAssignableFrom(t);

        return isContractExt;
    }

}
