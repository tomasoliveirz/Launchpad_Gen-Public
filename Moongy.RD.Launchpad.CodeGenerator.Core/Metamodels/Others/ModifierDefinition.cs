namespace Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
public class ModifierDefinition
{
    public required string Name { get; set; }
    public List<ParameterDefinition> Arguments { get; set; } = [];
}
