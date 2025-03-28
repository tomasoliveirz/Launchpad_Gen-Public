using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Generator.Contracts.SemiFungibleToken.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.SemiFungibleToken.Validators;

public static class DecimalsValidator
{
    public static void Validate(SemiFungibleTokenModel token)
    {
        if (token.Decimals < 0 || token.Decimals > 18)
            throw new InvalidTokenDecimalsException(0, 18);
    }
}