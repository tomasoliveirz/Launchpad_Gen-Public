using Moongy.RD.Launchpad.Core.Enums;

namespace Moongy.RD.Launchpad.Core.Models.Metamodel;

public class Argument (string name, VariableDataType type, string value)
{
    public string Name { get; set; } = name;
    public VariableDataType Type { get; set; } = type;
    public string Value { get; set; } = value;
}
