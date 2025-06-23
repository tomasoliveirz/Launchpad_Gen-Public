using Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Standards.ExtensionMethods;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Models;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Extractors;
public class FungibleTokenFeatureExtractor() : BaseStandardFeatureExtractor<FungibleTokenModel>(StandardEnum.FungibleToken), IFeatureExtractor<FungibleTokenModel>
{
    public override FungibleTokenModel Extract(object form)
    {
        var model = base.Extract(form);

        var premint = form.GetFormProperty<ulong>(StandardEnum.FungibleToken, nameof(FungibleTokenModel.Premint));
        var maxSupply = form.GetFormProperty<ulong>(StandardEnum.FungibleToken, nameof(FungibleTokenModel.MaxSupply));
        Console.WriteLine($"Premint: {premint}, MaxSupply: {maxSupply}");
        if (maxSupply > 0)
        {
            if (maxSupply == premint) model.SupplyType = SupplyType.Fixed;
            else model.SupplyType = SupplyType.Capped;
        }
        else model.SupplyType = SupplyType.Unlimited;
        return model;
    }
}
