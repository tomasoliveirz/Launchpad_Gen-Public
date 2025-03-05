using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Models;

namespace Moongy.RD.Launchpad.ContractGenerator.ModelComposers.AdvancedFungibleToken.Models
{
    public class AdvancedFungibleTokenModel : TokenModel
    {
        public string Symbol { get; set; }
        public decimal AntiWhale { get; set; }
        public bool HasAntiWhaleCap => AntiWhale != 0;
        public bool HasAutoSwap { get; set; }
        public long TotalSupply { get; set; }
        public bool HasFlashMint { get; set; }
        public bool IsHookable { get; set; }
        public bool HasLiquidityManagement { get; set; }
        public bool IsPausable { get; set; }
        public bool HasPremint { get; set; }
        public bool HasTokenRecovery { get; set; }
        public bool HasReflection { get; set; }
        public bool HasTax { get; set; }
        public decimal? TaxFee { get; set; }
        public bool IsUpgradable { get; set; }
        public bool Voting { get; set; }
    }
}
