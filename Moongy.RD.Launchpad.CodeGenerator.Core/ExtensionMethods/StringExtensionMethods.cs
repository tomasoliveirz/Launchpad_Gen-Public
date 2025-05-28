using System.Text.RegularExpressions;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.ExtensionMethods;

public static class StringExtensionMethods
{
    private static readonly Regex EthAddressRegex =
    new Regex("^0x[0-9a-fA-F]{40}$", RegexOptions.Compiled);

    public static bool IsEthAddress(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        if (!EthAddressRegex.IsMatch(input))
            return false;
        return true;
    }
}
