using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Attributes;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Models.Deflation;

namespace Moongy.RD.Launchpad.Data.Forms.Tokenomics
{
    [Tokenomic(Source = TokenomicEnum.Deflation)]
    public class DeflationTokenomic
    {
        [TokenomicProperty(Name = nameof(DeflationTokenomicModel.DeflationFeePercent), Source = TokenomicEnum.Deflation)]
        public double BurnFee { get; set; }
    }
}
