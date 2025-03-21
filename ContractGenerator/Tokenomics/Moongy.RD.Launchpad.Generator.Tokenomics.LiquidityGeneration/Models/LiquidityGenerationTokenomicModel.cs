using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.LiquidityGeneration.Models;
public class LiquidityGenerationTokenomicModel : ITokenomic
{
   public decimal LiquidityPercentage { get; set; }
}
