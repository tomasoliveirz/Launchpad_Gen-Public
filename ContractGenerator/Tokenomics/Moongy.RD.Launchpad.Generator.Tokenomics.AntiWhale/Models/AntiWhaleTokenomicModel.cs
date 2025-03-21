using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.AntiWhale.Models;
public class AntiWhaleTokenomicModel : ITokenomic
{
    public decimal MaxWalletPercentage { get; set; }
}
