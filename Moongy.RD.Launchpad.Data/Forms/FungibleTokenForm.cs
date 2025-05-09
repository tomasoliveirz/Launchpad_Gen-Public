using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.Data.Components;
using Moongy.RD.Launchpad.Data.Enums;

namespace Moongy.RD.Launchpad.Data.Forms
{
    public class FungibleTokenForm : TokenBaseModel
    {
        public decimal PreMint { get; set; }
        public bool Mintable { get; set; }
        public bool Burnable { get; set; }
        public bool Pausable { get; set; }
        public bool Callback { get; set; }
        public bool Permit { get; set; }
        public bool FlashMinting { get; set; }
        public int TaxFee { get; set; }
        public List<TaxRecipient> TaxRecipients { get; set; } = [];
        public int ReflectionFee { get; set; }
        public int BurnFee { get; set; }
        public int BuyBackFee { get; set; }
        public int LiquidityGenerationFee { get; set; }
        public int LiquidityRatio { get; set; }
        public VoteMode VoteMode { get; set; }
        public AccessControlType AccessControl { get; set; }
        public UpgradeabilityType Upgradeability { get; set; }
    }
}
