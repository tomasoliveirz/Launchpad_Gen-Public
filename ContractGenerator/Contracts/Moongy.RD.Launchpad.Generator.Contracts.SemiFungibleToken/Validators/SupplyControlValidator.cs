using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;
using Moongy.RD.Launchpad.Generator.Contracts.SemiFungibleToken.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.SemiFungibleToken.Validators;

public static class SupplyControlValidator
{
    public static void Validate(SemiFungibleTokenModel token)
    {
        if (!token.HasSupplyControl) return;
            
        if (token.MaxSupply == 0)
            throw new MaxSupplyException();
                
        if (!token.IsMintable)
            throw new SupplyControlException("Token must be mintable to have supply control.");
                
        AccessValidator.Validate(token.Access, true);
    }
}