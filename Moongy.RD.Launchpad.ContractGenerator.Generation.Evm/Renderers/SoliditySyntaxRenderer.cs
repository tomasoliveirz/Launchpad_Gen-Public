namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Renderers;

public static class SoliditySyntaxRenderer
{
    public static SolidityReferenceTypeSyntaxRenderer ReferenceType { get; } = new();
    public static SolidityImportSyntaxRenderer Import { get; } = new();

}