using Moongy.RD.Launchpad.Core.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Enums;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Core.Models
{
    public abstract class BaseTokenomicModel : ITokenomic
    {
        public double TaxPercentage { get; set; }
        public TokenomicTriggerMode TriggerMode { get; set; }
        public Address TaxCollector { get; set; } = new Address("0x0");
    }
}