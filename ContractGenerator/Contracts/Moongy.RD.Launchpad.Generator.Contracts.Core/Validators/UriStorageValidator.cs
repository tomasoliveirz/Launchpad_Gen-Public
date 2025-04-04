using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Enumerables;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;

public static class UriStorageValidator
{
    public static void Validate(IUriStorable token)
    {
        if (string.IsNullOrWhiteSpace(token.URI))
            throw new RequiredTokenURIException();

        if (string.IsNullOrWhiteSpace(token.URIStorageLocation))
            throw new RequiredURIStorageLocationException();
                
        URIValidator.Validate(token.URI, true);
            
        if (!Enum.IsDefined(typeof(UriStorageType), token.URIStorageType))
            throw new InvalidEnumException("URI storage type", token.URIStorageType);
    }
}