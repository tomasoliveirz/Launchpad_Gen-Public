using Moongy.RD.Launchpad.Core.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Buyback.Models;
public class BuybackTokenomicModel : BaseTokenomicModel
{
    public decimal BuybackPercentage { get; set; }
    public bool ShouldBurn { get; set; }
    public Address? AmmAddress { get; set; }
}
