namespace Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Models.LiquidityGeneration
{
    public class LiquidityGenerationTokenomicModel
    {
        public byte LiquidityFeePercent { get; set; }
        public ulong LiquidityThreshold { get; set; }
        public string? LiquidityRouter { get; set; }
    }
}
