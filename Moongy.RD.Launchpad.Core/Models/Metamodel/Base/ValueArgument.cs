using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.Core.Enums;

namespace Moongy.RD.Launchpad.Core.Models.Metamodel.Base
{
    public class ValueArgument(string value) : Argument("",DataType.None, DataLocation.None, value)
    {
    }
}
