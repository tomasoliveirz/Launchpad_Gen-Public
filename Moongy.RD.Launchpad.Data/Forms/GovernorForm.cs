using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.Data.Enums;

namespace Moongy.RD.Launchpad.Data.Forms
{
    public class GovernorForm:TokenBaseModel
    {
        public TimeSpan VotingDelay { get; set; }
        public TimeSpan VotingPeriod { get; set; }
        public int ProposalThreshold { get; set; }
        public double QuorumPercentage { get; set; }
        public int TokenDecimals { get; set; }
        public bool UpdatableSettings { get; set; }
        public bool Storage { get; set; }

        public GovernorVoteType VoteType { get; set; }
        public VoteMode TokenClockMode { get; set; }
        public TimelockType Timelock { get; set; }
        public UpgradeabilityType Upgradeability { get; set; }
    }
}
