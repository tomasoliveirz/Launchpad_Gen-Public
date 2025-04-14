namespace Moongy.RD.Launchpad.Core.Exceptions;

public class RequiredTokenURIException() : TokenValidationException("URI cannot be empty for a token.");