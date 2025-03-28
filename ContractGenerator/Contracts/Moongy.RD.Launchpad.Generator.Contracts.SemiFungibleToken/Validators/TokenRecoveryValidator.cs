using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;
using Moongy.RD.Launchpad.Generator.Contracts.SemiFungibleToken.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.SemiFungibleToken.Validators;

public static class TokenRecoveryValidator
{
    public static void Validate(SemiFungibleTokenModel token)
    {
        if (!token.HasTokenRecovery) return;
        if (!token.IsPausable)
            throw new TokenRecoveryException("Token recovery requires pausable functionality.");
                
        AccessValidator.Validate(token.Access, true);
    }
}