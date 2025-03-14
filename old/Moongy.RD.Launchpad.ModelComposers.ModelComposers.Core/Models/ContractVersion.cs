using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Models
{
    public class ContractVersion
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Revison { get; set; }
        public bool AllowCompileInLatestVersion { get; set; }
    }
}
