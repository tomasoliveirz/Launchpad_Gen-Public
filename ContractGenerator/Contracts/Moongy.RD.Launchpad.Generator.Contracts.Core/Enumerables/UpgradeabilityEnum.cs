using Moongy.RD.Launchpad.Core.Attributes;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Enumerables;

public enum UpgradeabilityEnum
{
    NONE,
    [OptionLabel(Label = "Transparent")]
    TRANSPARENT,
    [OptionLabel(Label = "UUPS")]
    UUPS,
}
