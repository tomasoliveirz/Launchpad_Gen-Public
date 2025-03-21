using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Enumerables;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces
{
    public interface IMintableToken
    {
        public bool IsMintable { get; set; }
        public TokenAccess? Access { get; set; }
    }
}
