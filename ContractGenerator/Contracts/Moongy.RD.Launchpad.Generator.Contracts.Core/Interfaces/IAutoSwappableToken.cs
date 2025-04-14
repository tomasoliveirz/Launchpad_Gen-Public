using Moongy.RD.Launchpad.Generator.Contracts.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces
{
    public interface IAutoSwappableToken
    {
        bool HasAutoSwap { get; set; }
        bool IsMintable { get; set; }
        TokenAccess? Access { get; set; }
    }

}
