using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others
{
    public class TriggerDefinition
    {
        public required string Name { get; set; }
        public List<ParameterDefinition> Parameters { get; set; } = [];
        public TriggerKind Kind { get; set; }
    }
}
