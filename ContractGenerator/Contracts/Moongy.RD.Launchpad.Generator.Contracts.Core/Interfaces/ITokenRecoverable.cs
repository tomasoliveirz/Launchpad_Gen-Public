
using Moongy.RD.Launchpad.Generator.Contracts.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces
{
    public interface ITokenRecoverable
    {
        bool HasTokenRecovery { get; set; }
        bool IsPausable { get; set; }
        TokenAccess? Access { get; set; }
    }
}
