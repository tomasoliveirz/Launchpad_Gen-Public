
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels;
using Moongy.RD.Launchpad.Core.Attributes;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Models
{
    public class BaseContractModel
    {
        [MetaModelName(Name = nameof(SolidityContractModel.Name))]
        public  string Name { get; set; }
    }
}
