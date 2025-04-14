namespace Moongy.RD.Launchpad.Core.Exceptions;

public class InvalidTokenSymbolFormatException(string symbol)
    : TokenValidationException($"Token symbol '{symbol}' must contain only alphanumeric characters.");