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

        // Extrair propriedades principais
        model.IsEnumerable = form.GetFormProperty<bool>(StandardEnum.NonFungibleToken, nameof(NonFungibleTokenModel.IsEnumerable));
        model.HasURI = form.GetFormProperty<bool>(StandardEnum.NonFungibleToken, nameof(NonFungibleTokenModel.HasURI));

        // Só extrair URI se HasURI for true
        if (model.HasURI)
        {
            model.URI = form.GetFormProperty<string>(StandardEnum.NonFungibleToken, nameof(NonFungibleTokenModel.URI));

            // Para o enum UriStorageType
            var uriStorageTypeString = form.GetFormProperty<string>(StandardEnum.NonFungibleToken, nameof(NonFungibleTokenModel.URIStorageType));
            if (Enum.TryParse<UriStorageType>(uriStorageTypeString, out var uriStorageType))
            {
                model.URIStorageType = uriStorageType;
            }

            // Extrair URIStorageLocation independentemente do tipo de armazenamento
            // Você pode ajustar a lógica aqui se apenas certos tipos de armazenamento precisarem de location
            model.URIStorageLocation = form.GetFormProperty<string>(StandardEnum.NonFungibleToken, nameof(NonFungibleTokenModel.URIStorageLocation));
        }

        return model;
    }
}