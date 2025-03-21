using Moongy.RD.Launchpad.ContractGenerator.Tokenomics.Core.Interfaces;

namespace Moongy.RD.Launchpad.ContractGenerator.Tokenomics.Tax.Models;
public class TaxTokenomicModel : ITokenomic
{
    public decimal TaxPercentage { get; set; }
    public ulong TaxAmount { get; set; }
}
