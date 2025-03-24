using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Core.Models
{
    public abstract class BaseTokenomicModel : ITokenomic
    {
        public int Weight { get; set; }
    }
}
