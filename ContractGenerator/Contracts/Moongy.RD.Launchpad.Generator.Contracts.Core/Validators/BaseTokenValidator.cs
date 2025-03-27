using Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;

public abstract class BaseTokenValidator<TToken> : ITokenValidator<TToken> where TToken : BaseTokenModel
{
    public virtual void Validate(TToken token)
    { 
        /*
        common validations
         - access
         - burn
         - mint
         - name
         - pausable
         - permit
         - upgradeability
         - voting 
        
        */
        NameValidator.Validate(token.Name); // done
        BurnValidator.Validate(token); // burn can be restricted or not ??? waiting for response - update: burn does not require access control
        MintValidator.Validate(token); // mint has to be restricted - access control required
        PausableValidator.Validate(token); // done
        PermitValidator.Validate(token); // how do we validate permit? 
        UpgradeabilityValidator.Validate(token); // we need to determine the enum values for upgradeability first and what types we will support
        VotingValidator.Validate(token.Voting); // how do we validate voting?

    }
}

