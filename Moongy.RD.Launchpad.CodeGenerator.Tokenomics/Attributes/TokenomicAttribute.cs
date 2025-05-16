using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Enums;

namespace Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TokenomicAttribute : Attribute
    {
        public required TokenomicEnum Source { get; set; }
    }
}
