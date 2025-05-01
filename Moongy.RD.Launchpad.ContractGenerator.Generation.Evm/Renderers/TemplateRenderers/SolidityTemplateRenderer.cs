using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Renderers.Templates;

//Not necessary after builder
public static class SolidityTemplateRenderer
{
    public static ContractConstructorRenderer Constructor { get; set; } = new();
    public static FileHeaderRenderer FileHeader { get; set; } = new();
    public static ImportProcessor Imports { get; set; } = new();
}
