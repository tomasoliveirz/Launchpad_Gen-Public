using Moongy.RD.Launchpad.Core.Enums;

namespace Moongy.RD.Launchpad.Core.Models.Metamodel.Base
{
    public class NameTypeArgument(string name, DataType type, DataLocation location) : Argument(name, type, location, "")
    {
    }
}
