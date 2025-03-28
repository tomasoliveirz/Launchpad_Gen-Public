using Moongy.RD.Launchpad.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Moongy.RD.Launchpad.Core.Models.Metamodel
{
    public class SmartContractFunction
    {
        public string? Name { get; set; }
        public List<Argument> SmartContractArguments { get; set; } = [];
        public VisibilityModifier SmartContractVisibility { get; set; }
        public bool IsOverrideble { get; set; }
        public VariableDataType ReturnType { get; set; }
        //Methods missing
    }
}
