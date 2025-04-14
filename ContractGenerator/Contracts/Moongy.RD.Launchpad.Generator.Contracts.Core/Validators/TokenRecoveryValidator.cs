using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;

public static class TokenRecoveryValidator
{
    public static void Validate(ITokenRecoverable token)
    {
        if (!token.HasTokenRecovery) return;
        if (!token.IsPausable)
            throw new TokenRecoveryException("Token recovery requires pausable functionality.");
                
        AccessValidator.Validate(token.Access, true);
    }
}