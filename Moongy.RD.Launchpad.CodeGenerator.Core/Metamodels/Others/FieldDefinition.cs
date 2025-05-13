namespace Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
public class FieldDefinition
{
    public required string Name { get; set; }
    public required TypeReference Type { get; set; }
    public required Visibility Visibility { get; set; }
    public bool IsImmutable { get; set; }
}
