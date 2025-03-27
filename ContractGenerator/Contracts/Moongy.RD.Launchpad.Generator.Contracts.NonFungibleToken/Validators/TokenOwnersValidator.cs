using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Core.Validators;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;
using Moongy.RD.Launchpad.Generator.Contracts.NonFungibleToken.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.NonFungibleToken.Validators;

public static class TokenOwnersValidator
{
    public static void Validate(NonFungibleTokenModel token)
    {
        if (!token.IsEnumerable) return;
            
        if (token.TokenOwners == null || token.TokenOwners.Count == 0)
            throw new EnumerableTokenException("Token is enumerable but no TokenOwners mapping provided.");
                
        foreach (var entry in token.TokenOwners)
        {
            if (entry.Key <= 0)
                throw new InvalidTokenIdException(entry.Key);
                    
            EthereumAddressValidator.Validate(entry.Value, true, "token owner");
        }
    }
}