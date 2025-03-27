using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Validators;

public static class FlashMintValidator
{
    public static void Validate(FungibleTokenModel token)
    {
        if (!token.HasFlashMint) return;
            
        if (!token.IsMintable)
            throw new FlashMintException("Token must be mintable to support flash mint.");
    }
}