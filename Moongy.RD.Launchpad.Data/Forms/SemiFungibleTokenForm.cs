using Moongy.RD.Launchpad.Data.Components;
using Moongy.RD.Launchpad.Data.Enums;

namespace Moongy.RD.Launchpad.Data.Forms
{
    public class SemiFungibleTokenForm : TokenBaseModel
    {
        public string? URI { get; set; }
        public bool Mintable { get; set; }
        public bool Burnable { get; set; }
        public bool SupplyTracking { get; set; }
        public bool Pausable { get; set; }
        public bool UpdatableURI { get; set; }
        public int TaxFee { get; set; }
        public List<TaxRecipient> TaxRecipients { get; set; } = [];
        public int ReflectionFee { get; set; }
        public int BurnFee { get; set; }
        public int BuyBackFee { get; set; }
        public int LiquidityGenerationFee { get; set; }
        public int LiquidityRatio { get; set; }
        public AccessControlType AccessControl { get; set; }
        public UpgradeabilityType Upgradeability { get; set; }
    }
}
