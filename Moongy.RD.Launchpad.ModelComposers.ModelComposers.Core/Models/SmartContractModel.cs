using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Models
{
    public class SmartContractModel
    {
        public string? License { get; set; }
        public string? Name { get; set; }
        public ContractVersion? MinimumVersion { get; set; }
        public ContractVersion? MaximumVersion { get; set; }
        public List<ContractAbstraction> Abstractions { get; set; } = [];
        public List<ContractProperty> Properties { get; set; }
        public List<ContractOperation> ConstructorOperations { get; set; } = [];
        public List<ContractFunction> Functions { get; set; } = [];
    }
}
