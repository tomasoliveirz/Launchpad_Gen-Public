namespace Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Interfaces;
public class InterfaceDefinition
{
    public required string Name { get; set; }
    public List<FunctionSignature> Signatures { get; set; } = [];
}
