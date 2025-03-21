using Moongy.RD.Launchpad.Core.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Reflections.Models;
public class ReflectionsTokenomicModel : ITokenomic
{
    public decimal ReflectionsPercentage { get; set; }
    public Dictionary<Address, ulong> UserClaimableBalances { get; set; } = new Dictionary<Address, ulong>();
}
