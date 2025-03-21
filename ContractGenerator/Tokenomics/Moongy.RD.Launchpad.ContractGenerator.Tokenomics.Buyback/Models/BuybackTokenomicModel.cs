using Moongy.RD.Launchpad.ContractGenerator.Tokenomics.Core.Interfaces;

namespace Moongy.RD.Launchpad.ContractGenerator.Tokenomics.Buyback.Models;
public class BuybackTokenomicModel : ITokenomic
{
    public ulong BuybackAmount { get; set; }
    public decimal BuybackPercentage { get; set; }
}
