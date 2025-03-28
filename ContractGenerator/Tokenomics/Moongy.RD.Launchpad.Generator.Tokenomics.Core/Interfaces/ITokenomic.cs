using Moongy.RD.Launchpad.Core.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Enums;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;
public interface ITokenomic
{
    public double TaxPercentage { get; set; }
    public TokenomicTriggerMode TriggerMode { get; set; }
    public Address TaxCollector { get; set; }
}
