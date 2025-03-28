using Moongy.RD.Launchpad.Core.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Enums;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Core.Models
{
    public abstract class BaseTokenomicModel : ITokenomic
    {
        public decimal TaxPercentage { get; set; }
        public TokenomicTriggerMode TriggerMode { get; set; }
        public Address TaxCollector { get; set; } = new("0x0");

    }
}
