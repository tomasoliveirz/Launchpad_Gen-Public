using System.Text.RegularExpressions;
using Moongy.RD.Launchpad.Core.Exceptions;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;

public static class SymbolValidator
{
    private static readonly Regex AlphanumericRegex = new Regex("^[a-zA-Z0-9]+$", RegexOptions.Compiled);
        
    public static void Validate(string? symbol, bool isRequired = false, string tokenType = "token", int minLength = 2, int maxLength = 5)
    {
        if (string.IsNullOrWhiteSpace(symbol))
        {
            if (isRequired)
                throw new RequiredTokenSymbolException(tokenType);
            return;
        }

        if (symbol.Length < minLength || symbol.Length > maxLength)
            throw new InvalidTokenSymbolException(symbol, minLength, maxLength);

        if (!AlphanumericRegex.IsMatch(symbol))
            throw new InvalidTokenSymbolFormatException(symbol);
    }
}