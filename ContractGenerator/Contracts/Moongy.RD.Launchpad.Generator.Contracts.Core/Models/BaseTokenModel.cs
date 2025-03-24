using Moongy.RD.Launchpad.ContractGenerator.Contracts.Core.Enumerables;
using Moongy.RD.Launchpad.Core.Models;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Enumerables;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Models
{
    public abstract class BaseTokenModel : IToken, IMintableToken, IBurnableToken
    {
        public string? Name { get; set; }
        public string? Symbol { get; set; }
        public bool IsPausable { get; set; }
        public bool IsPermit { get; set; }
        public bool IsMintable { get; set; }
        public bool IsBurnable { get; set; }
        public TokenAccess? Access { get; set; }



        public VotingEnum Voting { get; set; }
        public UpgradeabilityEnum Upgradeability { get; set; }


    }
}
