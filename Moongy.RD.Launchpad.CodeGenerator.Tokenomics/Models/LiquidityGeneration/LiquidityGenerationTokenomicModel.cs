using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Models.LiquidityGeneration
{
    public class LiquidityGenerationTokenomicModel
    {
        public byte LiquidityFeePercent { get; set; }
        public ulong LiquidityThreshold { get; set; }
        public string? LiquidityRouter { get; set; }
    }
}
