using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Validators;

public static class DecimalsValidator
{
    public static void Validate(FungibleTokenModel token)
    {
        if (token.Decimals < 0 || token.Decimals > 18)
            throw new InvalidTokenDecimalsException(0, 18);
    }
}