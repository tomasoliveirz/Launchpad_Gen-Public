using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Generator.Contracts.AdvancedFungibleToken.Models;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;

public static class TokenRecoveryValidator
{
    public static void Validate(AdvancedFungibleTokenModel token)
    {
        if (!token.IsPausable)
            throw new TokenRecoveryException("Token recovery requires pausable functionality.");
                
        AccessValidator.Validate(token.Access, true);
    }
}