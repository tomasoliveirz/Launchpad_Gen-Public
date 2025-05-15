namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.ScribanRenderingModels;

internal class ImportRenderModel
{
    public required string Path { get; set; }
    public string? Alias { get; set; }
    public bool HasAlias => !string.IsNullOrEmpty(Alias);
    public bool ImportsNamedElements => NamedElements.Count != 0;
    public List<string> NamedElements { get; set; } = [];
}
