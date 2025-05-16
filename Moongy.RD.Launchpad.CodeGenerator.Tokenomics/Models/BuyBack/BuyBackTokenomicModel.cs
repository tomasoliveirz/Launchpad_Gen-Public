using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Models.BuyBack
{
    public class BuyBackTokenomicModel
    {
        public byte BuyBackFeePercent { get; set; }
        public ulong BuyBackThreshold { get; set; }
    }
}
