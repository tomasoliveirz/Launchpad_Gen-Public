using Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Models;

namespace Moongy.RD.Launchpad.ContractGenerator.ModelComposers.SemiFungibleToken.Models
{
    public class SemiFungibleTokenModel : TokenModel
    {
        public string Symbol { get; set; }
        public decimal AntiWhale { get; set; }
        public bool HasAntiWhaleCap => AntiWhale != 0;
        public bool HasAutoSwap { get; set; }
        public int Decimals { get; set; }
        public bool LiquidityManagement { get; set; }
        public bool IsPausable { get; set; }
        public bool HasSupplyControl { get; set; }
        public bool HasTokenRecovery { get; set; }
        public bool HasReflection { get; set; }
        public bool HasTax { get; set; }
        public decimal? TaxFee { get; set; }
        public string URI { get; set; }
    }
}
