namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.ScribanRenderingModels;

public class ContractHeaderRenderingModel
{
    public required string Name { get; set; }
    public bool HasDependencies => Dependencies.Any();
    public IEnumerable<string> Dependencies { get; set; } = [];
}
