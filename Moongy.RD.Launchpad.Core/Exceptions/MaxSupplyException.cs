namespace Moongy.RD.Launchpad.Core.Exceptions;

public class MaxSupplyException()
    : TokenValidationException("MaxSupply must be greater than 0 if supply control is enabled.");