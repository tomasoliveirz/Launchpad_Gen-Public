using Moongy.RD.Launchpad.Core.Enums;
using Moongy.RD.Launchpad.Core.Models.Metamodel.Base;
using System.ComponentModel.DataAnnotations;

namespace Moongy.RD.Launchpad.Core.Models.Metamodel
{
    public class SmartContractFunction
    {
        public string? Name { get; set; }
        public List<Argument> SmartContractArguments { get; set; } = [];
        public VisibilityModifier SmartContractVisibility { get; set; }
        public bool IsOverrideble { get; set; }
        public ValueArgument? ReturnType { get; set; }
        public List<ContractOperation> Operations { get; set; } = [];
    }
}
