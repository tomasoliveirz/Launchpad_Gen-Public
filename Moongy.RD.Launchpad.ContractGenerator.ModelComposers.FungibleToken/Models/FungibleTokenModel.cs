using Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Models;

namespace Moongy.RD.Launchpad.ContractGenerator.ModelComposers.FungibleToken.Models;
public class FungibleTokenModel : TokenModel
{
    public string Name { get; set; }
    public string Symbol { get; set; }
    public decimal AntiWhale { get; set; }
    public bool HasAntiWhaleCap => AntiWhale != 0;
    public bool IsAccessible { get; set; }
    public bool HasAutoSwap { get; set; }
    public bool IsBurnable { get; set; }
    public long Supply { get; set; }
    public int Decimals { get; set; }
    public bool HasFlashMint { get; set; }
    public bool LiquidityManagement { get; set; }
    public bool IsMintable { get; set; }
    public bool IsPausable { get; set; }
    public bool HasPermission { get; set; }
    public bool HasPremint { get; set; }
    public bool HasTokenRecovery { get; set; }
    public bool HasReflection { get; set; }
    public bool HasTax { get; set; }
    public decimal? TaxFee { get; set; }
    public bool IsUpgradable { get; set; }
    public bool Voting { get; set; }
}
