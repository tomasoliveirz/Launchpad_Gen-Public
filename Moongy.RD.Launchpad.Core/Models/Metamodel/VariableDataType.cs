using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.Core.Enums;

namespace Moongy.RD.Launchpad.Core.Models.Metamodel
{
    public class VariableDataType(DataType type, DataLocation location)
    {
        DataType Type { get; set; } = type;
        DataLocation Location { get; set; } = location;
    }
}
