using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Renderers.Templates;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Renderers.TemplateRenderers;

//Not necessary after builder
public static class SolidityTemplateRenderer
{
    public static ContractHeaderProcessor ContractHeader { get; set; } = new();
    public static FileHeaderProcessor FileHeader { get; set; } = new();
    public static ImportProcessor Imports { get; set; } = new();
}
