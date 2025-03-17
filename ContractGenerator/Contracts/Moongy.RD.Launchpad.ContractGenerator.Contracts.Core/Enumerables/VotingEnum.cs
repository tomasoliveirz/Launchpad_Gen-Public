using Moongy.RD.Launchpad.Core.Attributes;

namespace Moongy.RD.Launchpad.ContractGenerator.Contracts.Core.Enumerables;
public enum VotingEnum
{
    NONE,
    [OptionLabel(Label = "Block Number")]
    BLOCK_NUMBER,
    [OptionLabel(Label = "Timestamp")]
    TIMESTAMP,
}
