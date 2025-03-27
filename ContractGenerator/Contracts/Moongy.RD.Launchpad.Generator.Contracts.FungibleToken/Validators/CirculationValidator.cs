using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Validators;

public static class CirculationValidator
{
    public static void Validate(FungibleTokenModel token)
    {
        if (token.Circulation > 0 && !token.IsMintable)
            throw new CirculationRequiresMintableException();
    }
}