using Moongy.RD.Launchpad.Tools.TokenWeighter.Models;

namespace Moongy.RD.Launchpad.Tools.TokenWeighter
{
    public interface ITokenWeighter
    {
        public TokenWeighterResponse GetWeight(TokenWeighterRequest request);
    }
}
