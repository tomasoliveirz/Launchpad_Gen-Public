using System.ComponentModel.DataAnnotations;
using Moongy.RD.Launchpad.Core.Attributes;

namespace Moongy.RD.Launchpad.ContractGenerator.Contracts.Core.Enumerables;
public enum AccessEnum
{
    NONE,
    [OptionLabel(Label = "Ownable")]
    OWNABLE,
    [OptionLabel(Label = "Role")]
    ROLE,
    [OptionLabel(Label = "Managed")]
    MANAGED
}
