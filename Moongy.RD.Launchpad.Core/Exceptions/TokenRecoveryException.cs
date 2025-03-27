namespace Moongy.RD.Launchpad.Core.Exceptions;

public class TokenRecoveryException(string message) : TokenValidationException(message);