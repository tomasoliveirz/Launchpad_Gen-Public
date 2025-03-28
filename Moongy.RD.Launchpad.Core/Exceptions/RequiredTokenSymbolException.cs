namespace Moongy.RD.Launchpad.Core.Exceptions;

public class RequiredTokenSymbolException(string tokenType)
    : TokenValidationException($"Token symbol cannot be empty for a {tokenType}.");