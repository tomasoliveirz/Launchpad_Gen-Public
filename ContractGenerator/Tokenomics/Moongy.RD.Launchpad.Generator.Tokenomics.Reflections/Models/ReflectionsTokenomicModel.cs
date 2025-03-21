using Moongy.RD.Launchpad.Core.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Reflections.Models;
public class ReflectionsTokenomicModel : ITokenomic
{
    public enum ReflectionsType
    {
        Static,
        Dynamic
    }
    public decimal ReflectionsPercentage { get; set; }
    public ReflectionsType Type { get; set; }
}
