using Moongy.RD.Launchpad.Data.Enums;

namespace Moongy.RD.Launchpad.Data.Forms
{
    public class NonFungibleTokenForm : TokenBaseModel
    {
        public string? BaseURI { get; set; }
        public bool Mintable { get; set; }
        public bool AutoIncrementIds { get; set; }
        public bool Burnable { get; set; }
        public bool Pausable { get; set; }
        public bool Enumerable { get; set; }
        public bool URIStorage { get; set; }

        public VoteMode VoteMode { get; set; }
        public AccessControlType AccessControl { get; set; }
        public UpgradeabilityType Upgradeability { get; set; }
    }
}
