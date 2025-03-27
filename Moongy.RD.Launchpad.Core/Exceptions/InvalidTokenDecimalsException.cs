namespace Moongy.RD.Launchpad.Core.Exceptions;

public class InvalidTokenDecimalsException(int min, int max)
    : TokenValidationException($"Token decimals must be between {min} and {max}.");