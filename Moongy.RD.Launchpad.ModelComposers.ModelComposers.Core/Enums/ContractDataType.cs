using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Enums
{
    public class ContractDataType
    {
        public ContractDataTypeEnum Type { get; set; }
        public List<ContractDataType> SubTypes { get; set; }
        public string CustomType { get; set; }
    }
}
