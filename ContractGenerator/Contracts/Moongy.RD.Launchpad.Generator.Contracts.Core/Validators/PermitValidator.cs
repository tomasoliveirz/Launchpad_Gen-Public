using Moongy.RD.Launchpad.Generator.Contracts.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;

public static class PermitValidator
{
    public static void Validate(BaseTokenModel token)
    {
        if (!token.IsPermit) return;
    }
}