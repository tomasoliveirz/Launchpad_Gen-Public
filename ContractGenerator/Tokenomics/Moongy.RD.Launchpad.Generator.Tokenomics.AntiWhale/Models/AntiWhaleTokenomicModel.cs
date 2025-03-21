using Moongy.RD.Launchpad.ContractGenerator.Tokenomics.Core.Interfaces;

namespace Moongy.RD.Launchpad.ContractGenerator.Tokenomics.AntiWhale.Models;
public class AntiWhaleTokenomicModel : ITokenomic
{
    public ulong MaxTransactionAmount { get; set; }
    public decimal MaxWalletPercentage { get; set; }
}
