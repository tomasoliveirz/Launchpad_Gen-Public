namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Renderers.ComplexExpressions;

public static class SoliditySyntaxRenderer
{
    public static SolidityReferenceTypeSyntaxRenderer ReferenceType { get; } = new();
    public static SolidityImportSyntaxRenderer Import { get; } = new();
    public static SolidityVersionSyntaxRenderer Version { get; } = new();
    public static SolidityParameterSyntaxRenderer Parameters { get; } = new();

}