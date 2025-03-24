using Moongy.RD.Launchpad.ContractGenerator.Contracts.Core.Enumerables;
using Moongy.RD.Launchpad.Core.Models;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Enumerables;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Models
{
    public class TokenAccess
    {
        public AccessEnum Type { get; set; }
        public List<Role> Roles { get; set; } = [];
        public List<Address> Managers { get; set; } = [];
        public Address? Owner { get; set; }
    }
}
