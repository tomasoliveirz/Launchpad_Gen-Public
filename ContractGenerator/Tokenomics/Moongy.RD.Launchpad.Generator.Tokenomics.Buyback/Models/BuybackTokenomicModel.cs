using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Buyback.Models;
public class BuybackTokenomicModel : ITokenomic
{
    public enum BuybackType
    {
        Static,
        Dynamic
    }
    public decimal BuybackPercentage { get; set; }
    public BuybackType Type { get; set; }
}
