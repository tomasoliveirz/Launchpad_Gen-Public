using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.TypeReferences;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Imports
{
    public class TypeUtilityImportModel : ImportModel
    {
        public required string Name { get; set; }
        public required TypeReference Type { get; set; }
    }

}
