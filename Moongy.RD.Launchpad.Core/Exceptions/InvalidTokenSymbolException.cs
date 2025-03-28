namespace Moongy.RD.Launchpad.Core.Exceptions
{
    public class InvalidTokenSymbolException(string symbol, int minLength, int maxLength)
        : TokenValidationException($"Token symbol '{symbol}' must be between {minLength} and {maxLength} characters.");
}

