using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Attributes;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Models.LiquidityGeneration;

namespace Moongy.RD.Launchpad.Data.Forms.Tokenomics
{
    [Tokenomic(Source = TokenomicEnum.LiquidityGeneration)]
    public class LiquidityGenerationTokenomic
    {
        [TokenomicProperty(Name = nameof(LiquidityGenerationTokenomicModel.LiquidityFeePercent), Source = TokenomicEnum.LiquidityGeneration)]
        public double LiquidityFee { get; set; }

        [TokenomicProperty(Name = nameof(LiquidityGenerationTokenomicModel.LiquidityThreshold), Source = TokenomicEnum.LiquidityGeneration)]
        public decimal Threshold { get; set; }

        [TokenomicProperty(Name = nameof(LiquidityGenerationTokenomicModel.LiquidityRouter), Source = TokenomicEnum.LiquidityGeneration)]
        public string RouterAddress { get; set; } = string.Empty;
    }
}
