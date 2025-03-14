using Moongy.RD.Launchpad.Decorators.Core.Interfaces;

namespace Moongy.RD.Launchpad.Decorators.Tokenomics.AntiWhale.Models;
public class AntiWhaleTokenomic : ITokenomic
{
    public decimal? MaxPercentage { get; set; }
}
