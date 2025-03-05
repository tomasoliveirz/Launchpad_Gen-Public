using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Models
{
    public class TokenModel
    {
        public bool IsAccessible { get; set; }
        public bool IsBurnable { get; set; }
        public bool IsMintable { get; set; }
        public string Name { get; set; }
        public bool IsPausable { get; set; }
        public bool HasPermission { get; set; }

    }
}
