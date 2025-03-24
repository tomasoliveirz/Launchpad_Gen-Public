using Moongy.RD.Launchpad.Generator.Contracts.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces
{
    public interface IBurnableToken
    {
        public bool IsBurnable { get; set; }
        public TokenAccess? Access { get; set; }
    }
}
