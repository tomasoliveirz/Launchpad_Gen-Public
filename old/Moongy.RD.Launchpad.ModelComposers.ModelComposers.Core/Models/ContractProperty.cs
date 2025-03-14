using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Enums;

namespace Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Models
{
    public class ContractProperty
    {
        public ContractDataType Type { get; set; }
        public string Name { get; set; }
        public string DefaultValue { get; set; }
        public ContractVisibilityModifierEnum Visibility { get; set; }
        public List<ContractEvent> Events { get; set; } = [];
        public bool IsMandatory { get; set; }

    }
}
