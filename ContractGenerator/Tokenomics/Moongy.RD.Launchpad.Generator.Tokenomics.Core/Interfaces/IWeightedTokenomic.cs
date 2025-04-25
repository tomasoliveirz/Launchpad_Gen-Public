using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces
{
    public interface IWeightedTokenomic<T> where T : BaseTokenomicModel
    {
        double GetWeight(T value);
    }
}
