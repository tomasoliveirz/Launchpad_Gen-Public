namespace Moongy.RD.Launchpad.Core.Exceptions;

public class RequiredTokenNameException() : TokenValidationException("Token name cannot be null or empty.");