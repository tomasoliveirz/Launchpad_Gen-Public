namespace Moongy.RD.Launchpad.Core.Exceptions;

public class CirculationRequiresMintableException()
    : TokenValidationException("Token must be mintable to have initial circulation.");