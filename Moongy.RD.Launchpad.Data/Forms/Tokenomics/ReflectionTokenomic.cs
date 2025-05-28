using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Attributes;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Models.Reflection;

namespace Moongy.RD.Launchpad.Data.Forms.Tokenomics
{
    [Tokenomic(Source = TokenomicEnum.Reflection)]
    public class ReflectionTokenomic
    {
        [TokenomicProperty(Name = nameof(ReflectionTokenomicModel.ReflectionFeePercent), Source = TokenomicEnum.Reflection)]
        public double ReflectionFee { get; set; }

    }
}
