using Moongy.RD.Launchpad.Core.Enums;

namespace Moongy.RD.Launchpad.Core.Models.Metamodel;

public class Argument (string name, DataType type, DataLocation location, string value)
{
    public string Name { get; set; } = name;
    DataType Type { get; set; } = type;
    DataLocation Location { get; set; } = location;
    public string Value { get; set; } = value;
}
