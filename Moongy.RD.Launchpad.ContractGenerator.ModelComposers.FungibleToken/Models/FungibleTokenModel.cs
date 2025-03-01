using Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Models;

namespace Moongy.RD.Launchpad.ContractGenerator.ModelComposers.FungibleToken.Models;
public class FungibleTokenModel : TokenModel
{
    public string Name { get; set; }
    public string Symbol { get; set; }
    public decimal AntiWhale { get; set; }
    public bool HasAntiWhaleCap => AntiWhale != 0;
}
