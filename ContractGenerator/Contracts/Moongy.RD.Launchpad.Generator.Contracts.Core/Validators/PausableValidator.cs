using Moongy.RD.Launchpad.Generator.Contracts.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;

public static class PausableValidator
{
    public static void Validate(BaseTokenModel token)
    {
        if (!token.IsPausable) return;
        
        AccessValidator.Validate(token.Access, true);
    }
}