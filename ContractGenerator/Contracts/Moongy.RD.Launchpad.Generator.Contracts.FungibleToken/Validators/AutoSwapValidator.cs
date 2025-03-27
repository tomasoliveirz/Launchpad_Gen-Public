using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;
using Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Validators;

public static class AutoSwapValidator
{
    public static void Validate(FungibleTokenModel token)
    {
        if (!token.HasAutoSwap) return;
            
        if (!token.IsMintable)
            throw new AutoSwapException("Token must be mintable to support auto swap.");
                
        AccessValidator.Validate(token.Access, true);
    }
}