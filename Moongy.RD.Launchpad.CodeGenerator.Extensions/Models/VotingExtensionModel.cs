using Moongy.RD.Launchpad.CodeGenerator.Extensions.Enums;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Models;
public class VotingExtensionModel
{
    public VotingTypeEnum Type { get; set; }

    #region For Governor only
    public int VotingDelayInHours { get; set; }
    public int VotingPeriodInHours { get; set; }

    public int Quorum {  get; set; }
    public bool QuorumIsPercentage { get; set; } = false;
    public VotingTimeLock TimeLock { get; set; }
    #endregion
}
