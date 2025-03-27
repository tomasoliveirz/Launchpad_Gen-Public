using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Reflections.Models;
public class ReflectionsTokenomicModel : BaseTokenomicModel
{
    public enum ReflectionsType
    {
        Static,
        Dynamic
    }
    public decimal ReflectionsPercentage { get; set; }
    public ReflectionsType Type { get; set; }
}
