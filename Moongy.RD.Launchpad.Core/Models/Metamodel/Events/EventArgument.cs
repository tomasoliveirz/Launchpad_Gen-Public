
using Moongy.RD.Launchpad.Core.Enums;

namespace Moongy.RD.Launchpad.Core.Models.Metamodel.Events
{
    public class EventArgument(string name, VariableDataType type) : Argument(name, type, "")
    {
    }
}
