using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;

public static class DecimalsValidator
{
    public static void Validate(IDecimalToken token)
    {
        if (token.Decimals < 0 || token.Decimals > 18)
            throw new InvalidTokenDecimalsException(0, 18);
    }
}