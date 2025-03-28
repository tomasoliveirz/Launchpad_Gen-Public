namespace Moongy.RD.Launchpad.Core.Exceptions;

public class NullTokenException(string tokenType) : TokenValidationException($"{tokenType} cannot be null.");