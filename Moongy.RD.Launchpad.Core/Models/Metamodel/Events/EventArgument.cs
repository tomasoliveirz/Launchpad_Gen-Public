
using Moongy.RD.Launchpad.Core.Enums;

namespace Moongy.RD.Launchpad.Core.Models.Metamodel.Events
{
    public class EventArgument(string name, DataType type) : Argument(name, type, "")
    {
    }
}
