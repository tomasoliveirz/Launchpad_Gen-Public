namespace Moongy.RD.Launchpad.Core.Exceptions;

public class InvalidTokenNameException(string name)
    : TokenValidationException($"Token name '{name}' must be between 1 and 50 characters.");