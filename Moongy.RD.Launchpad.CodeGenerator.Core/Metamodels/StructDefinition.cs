using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;
public class StructDefinition
{
    public required string Name { get; set; }
    public List<ParameterDefinition> TypeParameters { get; set; } = [];
    public List<FieldDefinition> Fields { get; set; } = [];
}
