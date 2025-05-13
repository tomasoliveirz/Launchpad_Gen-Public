namespace Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
public class ParameterDefinition
{
    public required string Name { get; set; }
    public string? Value { get; set; }
    public required TypeReference Type { get; set; }
}
