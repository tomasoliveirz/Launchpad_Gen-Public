using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Deflation.Models;
public class DeflationTokenomicModel : ITokenomic
{
    public decimal BurnPercentage { get; set; }
}
