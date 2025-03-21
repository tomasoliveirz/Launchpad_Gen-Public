using Moongy.RD.Launchpad.ContractGenerator.Contracts.Core.Interfaces;

namespace Moongy.RD.Launchpad.ContractGenerator.Contracts.Core.Models
{
    public abstract class BaseTokenModel : IToken
    {
        public String? Name { get; set; }
        public String? Symbol { get; set; }
        public Dictionary<String,List<String>> Access { get; set; }
        public bool IsPausable { get; set; }
        public bool HasPermission { get; set; }
        public Dictionary<String, Dictionary<String,ulong>> Permisssion { get; set; }
        public bool IsUpgradable { get; set; }
        public bool HasVotes { get; set; }
        public bool IsMintable { get; set; }
        public bool HasAccess { get; set; }
    }
}
