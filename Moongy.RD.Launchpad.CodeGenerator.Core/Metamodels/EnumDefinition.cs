using Moongy.RD.Launchpad.CodeGenerator.Core.Attributes;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;
public class EnumDefinition
{
    public required string Name { get; set; }
    
    [AtLeastOne]
    public List<string> Members { get; set; } = [];
}
