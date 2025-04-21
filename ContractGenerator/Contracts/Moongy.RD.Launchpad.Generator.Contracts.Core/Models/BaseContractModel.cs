
using Moongy.RD.Launchpad.Core.Attributes;
using Moongy.RD.Launchpad.Core.Models.Metamodel;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Models
{
    public class BaseContractModel
    {
        [MetaModelName(Name = nameof(SmartContractModel.Name))]
        public string Name { get; set; }
    }
}
