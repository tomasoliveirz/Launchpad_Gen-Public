using Moongy.RD.Launchpad.ContractGenerator.Tokenomics.Core.Interfaces;

namespace Moongy.RD.Launchpad.ContractGenerator.Tokenomics.Deflation.Models;
public class DeflationTokenomicModel : ITokenomic
{
    public decimal BurnPercentage { get; set; }
    public ulong BurnAmount { get; set; }
}
