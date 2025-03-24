using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Tax.Models;
public class TaxTokenomicModel : BaseTokenomicModel
{
    public enum TaxType
    {
        Static,
        Dynamic
    }
    public decimal TaxPercentage { get; set; }
    public TaxType Type { get; set; }
}
