using Moongy.RD.Launchpad.CodeGenerator.Standards.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Standards.ExtensionMethods;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Extractors;

public static class FeatureExtractor
{
    public static FungibleTokenFeatureExtractor FungibleToken { get; set; } = new();


    public static StandardEnum GetStandard(object form)
    {
        var standard = form.GetStandard() ?? throw new Exception("No standard found");
        return standard;
    }
}
