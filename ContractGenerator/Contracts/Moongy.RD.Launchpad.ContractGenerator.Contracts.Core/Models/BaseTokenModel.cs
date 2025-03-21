using Moongy.RD.Launchpad.ContractGenerator.Contracts.Core.Interfaces;

namespace Moongy.RD.Launchpad.ContractGenerator.Contracts.Core.Models
{
    public abstract class BaseTokenModel : IToken
    {
        String? Name { get; set; }
        String? Symbol { get; set; }
        Dictionary<String,List<String>> Access { get; set; }
        bool IsPausable { get; set; }
        bool HasPermission { get; set; }
        Dictionary<String, Dictionary<String,ulong>> Permisssion { get; set; }
        bool IsUpgradable { get; set; }
        bool HasVotes { get; set; }
        bool IsMintable { get; set; }
    }
}
