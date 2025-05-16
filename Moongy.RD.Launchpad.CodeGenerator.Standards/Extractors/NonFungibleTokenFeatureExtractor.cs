using Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Standards.ExtensionMethods;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Models;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Extractors;

public class NonFungibleTokenFeatureExtractor() : BaseStandardFeatureExtractor<NonFungibleTokenModel>(StandardEnum.NonFungibleToken), IFeatureExtractor<NonFungibleTokenModel>
{
    public override NonFungibleTokenModel Extract(object form)
    {
        var model = base.Extract(form);

        model.IsEnumerable = form.GetFormProperty<bool>(StandardEnum.NonFungibleToken, nameof(NonFungibleTokenModel.IsEnumerable));
        model.HasURI = form.GetFormProperty<bool>(StandardEnum.NonFungibleToken, nameof(NonFungibleTokenModel.HasURI));

        if (model.HasURI)
        {
            model.URI = form.GetFormProperty<string>(StandardEnum.NonFungibleToken, nameof(NonFungibleTokenModel.URI));

            model.URIStorageType = form.GetFormProperty<UriStorageType>(StandardEnum.NonFungibleToken, nameof(NonFungibleTokenModel.URIStorageType));

            model.URIStorageLocation = form.GetFormProperty<string>(StandardEnum.NonFungibleToken, nameof(NonFungibleTokenModel.URIStorageLocation));
        }

        return model;
    }
}