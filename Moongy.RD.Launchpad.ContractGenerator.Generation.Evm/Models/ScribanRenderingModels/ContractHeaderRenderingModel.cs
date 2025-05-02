namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.ScribanRenderingModels;

public class ContractHeaderRenderingModel
{
    public required string Name { get; set; }
    public bool HasDependencies => Dependencies.Any();
    public IEnumerable<string> Dependencies { get; set; } = [];
}
