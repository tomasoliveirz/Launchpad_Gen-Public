using Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;

public static class BurnValidator
{
    public static void Validate(IBurnableToken token)
    {
        if (!token.IsBurnable) return;
    }
}