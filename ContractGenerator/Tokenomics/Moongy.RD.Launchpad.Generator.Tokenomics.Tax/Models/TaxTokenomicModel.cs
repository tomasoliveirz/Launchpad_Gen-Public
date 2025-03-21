using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Tax.Models;
public class TaxTokenomicModel : ITokenomic
{
    public enum TaxType
    {
        Static,
        Dynamic
    }
    public decimal TaxPercentage { get; set; }
    public TaxType Type { get; set; }
}
