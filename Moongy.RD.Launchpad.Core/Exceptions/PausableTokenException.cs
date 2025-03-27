namespace Moongy.RD.Launchpad.Core.Exceptions;

public class PausableTokenException(string message) : TokenValidationException(message);