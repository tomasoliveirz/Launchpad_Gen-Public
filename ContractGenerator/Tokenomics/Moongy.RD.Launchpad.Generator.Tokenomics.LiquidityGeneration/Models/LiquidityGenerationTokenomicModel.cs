using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.LiquidityGeneration.Models;
public class LiquidityGenerationTokenomicModel : BaseTokenomicModel
{
    public enum LiquidityType
    {
        Static,
        Dynamic
    }
    public LiquidityType Type { get; set; }
    public decimal LiquidityPercentage { get; set; }
}
