using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.LiquidityGeneration.Models;
public class LiquidityGenerationTokenomicModel : ITokenomic
{
    public enum LiquidityType
    {
        Static,
        Dynamic
    }
    public LiquidityType Type { get; set; }
    public decimal LiquidityPercentage { get; set; }
}
