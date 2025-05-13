namespace Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Directives;
public class DirectiveDefinition
{
    public required string Value { get; set; }
    public string? Label { get; set; }
    public DirectiveKind Kind { get; set; }
}
