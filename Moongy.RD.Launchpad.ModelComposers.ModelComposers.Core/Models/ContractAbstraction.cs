using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Enums;

namespace Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Models
{
    public class ContractAbstraction
    {
        public string? Name { get; set; }
        public string? Path { get; set; }
        public List<ContractArgument> Arguments { get; set; } = [];

   
    }
}
