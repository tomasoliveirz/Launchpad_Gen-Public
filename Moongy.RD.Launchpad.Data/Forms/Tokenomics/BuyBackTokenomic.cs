using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Attributes;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Models.BuyBack;

namespace Moongy.RD.Launchpad.Data.Forms.Tokenomics
{
    [Tokenomic(Source = TokenomicEnum.BuyBack)]
    public class BuyBackTokenomic
    {
        [TokenomicProperty(Name = nameof(BuyBackTokenomicModel.BuyBackFeePercent), Source = TokenomicEnum.BuyBack)]
        public double BuyBackFee { get; set; }

        [TokenomicProperty(Name = nameof(BuyBackTokenomicModel.BuyBackThreshold), Source = TokenomicEnum.BuyBack)]
        public decimal Threshold { get; set; }
    }
}
