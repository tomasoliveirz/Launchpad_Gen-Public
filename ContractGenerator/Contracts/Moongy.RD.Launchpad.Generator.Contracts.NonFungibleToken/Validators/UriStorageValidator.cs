using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;
using Moongy.RD.Launchpad.Generator.Contracts.NonFungibleToken.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.NonFungibleToken.Validators;

public static class UriStorageValidator
{
    public static void Validate(NonFungibleTokenModel token)
    {
        if (string.IsNullOrWhiteSpace(token.URI))
            throw new RequiredTokenURIException();

        if (string.IsNullOrWhiteSpace(token.URIStorageLocation))
            throw new RequiredURIStorageLocationException();
                
        URIValidator.Validate(token.URI, true);
            
        if (!Enum.IsDefined(typeof(NonFungibleTokenModel.UriStorageType), token.URIStorageType))
            throw new InvalidEnumException("URI storage type", token.URIStorageType);
    }
}