using Moongy.RD.Launchpad.Core.Attributes;
using Moongy.RD.Launchpad.Core.Enums;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Enumerables;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Models
{
    public abstract class BaseTokenModel : BaseContractModel, IToken, IMintableToken, IBurnableToken
    {
        [MetaModelProperty(Name = nameof(IsPausable), PropertyType = PropertyType.Flag, DataType = DataType.Boolean)]
        public bool IsPausable { get; set; }

        [MetaModelProperty(Name = nameof(IsPermit), PropertyType = PropertyType.Flag, DataType = DataType.Boolean)]
        public bool IsPermit { get; set; }

        [MetaModelProperty(Name = nameof(IsMintable), PropertyType = PropertyType.Flag, DataType = DataType.Boolean)]
        public bool IsMintable { get; set; }

        [MetaModelProperty(Name = nameof(IsBurnable), PropertyType = PropertyType.Flag, DataType = DataType.Boolean)]
        public bool IsBurnable { get; set; }

        [MetaModelProperty(Name = nameof(Access), PropertyType = PropertyType.Option, DataType = DataType.Custom)]
        public TokenAccess? Access { get; set; }

        [MetaModelProperty(Name = nameof(Voting), PropertyType = PropertyType.Option, DataType = DataType.Enum)]
        public VotingEnum Voting { get; set; }

        [MetaModelProperty(Name = nameof(Upgradeability), PropertyType = PropertyType.Option, DataType = DataType.Enum)]
        public UpgradeabilityEnum Upgradeability { get; set; }
    }
}
