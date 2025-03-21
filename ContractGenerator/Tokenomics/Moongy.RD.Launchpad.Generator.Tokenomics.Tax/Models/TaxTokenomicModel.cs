using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Tax.Models;
public class TaxTokenomicModel : ITokenomic
{
    public decimal TaxPercentage { get; set; }
}
