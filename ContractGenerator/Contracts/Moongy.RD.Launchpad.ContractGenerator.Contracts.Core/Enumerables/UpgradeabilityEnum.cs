using Moongy.RD.Launchpad.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.Launchpad.ContractGenerator.Contracts.Core.Enumerables;

public enum UpgradeabilityEnum
{
    NONE,
    [OptionLabel(Label = "Transparent")]
    TRANSPARENT,
    [OptionLabel(Label = "UUPS")]
    UUPS,
}
