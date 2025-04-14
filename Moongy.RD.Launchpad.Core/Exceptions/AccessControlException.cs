namespace Moongy.RD.Launchpad.Core.Exceptions;

public class AccessControlException(string message) : TokenValidationException(message);