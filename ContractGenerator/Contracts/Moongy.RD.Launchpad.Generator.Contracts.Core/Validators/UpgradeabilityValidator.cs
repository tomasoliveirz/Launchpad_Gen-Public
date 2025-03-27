using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Enumerables;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;

public static class UpgradeabilityValidator
{
    public static void Validate(BaseTokenModel token)
    {   
        AccessValidator.Validate(token.Access, true);
        if (!Enum.IsDefined(typeof(UpgradeabilityEnum), token.Upgradeability))
            throw new InvalidEnumException("upgradeability", token.Upgradeability);
    }
}