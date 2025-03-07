using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Enums;

namespace Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Models
{
    public class ContractFunction
    {
        public string? Name { get; set; }
        public List<ContractArgument> Arguments { get; set; } = [];
        public ContractDataType ReturnType { get; set; }
        public ContractVisibilityModifierEnum Visibility { get; set; }
        public List<ContractOperation> Operations { get; set; }
        public bool IsOverridable { get; set; }
        public bool Overrides { get; set; }
    }
}
