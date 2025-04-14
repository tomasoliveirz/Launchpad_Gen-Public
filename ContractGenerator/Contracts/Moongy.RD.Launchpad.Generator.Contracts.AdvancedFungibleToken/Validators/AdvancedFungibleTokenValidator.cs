using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Generator.Contracts.AdvancedFungibleToken.Models;
using Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Validators;


namespace Moongy.RD.Launchpad.Generator.Contracts.AdvancedFungibleToken.Validators;

public class AdvancedFungibleTokenValidator : FungibleTokenValidator
{
    public void Validate(AdvancedFungibleTokenModel token)
    {
        base.Validate(token);
        
        PreTransferHooksValidator.Validate(token);
        PostTransferHooksValidator.Validate(token);
    }
}
