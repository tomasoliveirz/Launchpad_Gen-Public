using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Validators;

public static class PremintValidator
{
    public static void Validate(FungibleTokenModel token)
    {
        if (token.PremintAmount <= 0) return;
        if (!token.IsMintable)
            throw new PremintRequiresMintableException();
    }
}