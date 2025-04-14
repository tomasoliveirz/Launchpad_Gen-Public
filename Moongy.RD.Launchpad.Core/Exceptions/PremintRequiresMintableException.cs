namespace Moongy.RD.Launchpad.Core.Exceptions;

public class PremintRequiresMintableException()
    : TokenValidationException("Token must be mintable to have a premint amount.");