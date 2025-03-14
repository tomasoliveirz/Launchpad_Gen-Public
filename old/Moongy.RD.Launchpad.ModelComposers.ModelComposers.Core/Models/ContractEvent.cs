using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Models
{
    public class ContractEvent
    {
        public string? Name { get; set; }
        public List<ContractArgument> Arguments { get; set; } = [];
    }
}
