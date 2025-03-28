using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;
using Moongy.RD.Launchpad.Generator.Contracts.SemiFungibleToken.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.SemiFungibleToken.Validators;

public static class UriStorageValidator
{
    public static void Validate(SemiFungibleTokenModel token)
    {
        if (string.IsNullOrWhiteSpace(token.URI))
            throw new RequiredTokenURIException();

        if (string.IsNullOrWhiteSpace(token.URIStorageLocation))
            throw new RequiredURIStorageLocationException();
                
        UriValidator.Validate(token.URI, true);
            
        if (!Enum.IsDefined(typeof(SemiFungibleTokenModel.UriStorageType), token.URIStorageType))
            throw new InvalidEnumException("URI storage type", token.URIStorageType);
    }
}