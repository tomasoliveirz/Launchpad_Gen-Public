using Moongy.RD.Launchpad.Core.Enums;

namespace Moongy.RD.Launchpad.Core.Models.Metamodel.Base
{
    public class NameTypeArgument(string name, VariableDataType type) : Argument(name, type, "")
    {
    }
}
