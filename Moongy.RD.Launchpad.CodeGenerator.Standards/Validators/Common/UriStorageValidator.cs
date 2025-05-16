using System.ComponentModel.DataAnnotations;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Interfaces;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Validators.Common;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;

public static class UriStorageValidator
{
    public static void Validate(IUriStorable token)
    {
        if (string.IsNullOrWhiteSpace(token.URI))
            throw new ValidationException("Token URI is required.");

        if (string.IsNullOrWhiteSpace(token.URIStorageLocation))
            throw new ValidationException("URI storage location is required.");
                
        UriValidator.Validate(token.URI, true);
            
        if (!Enum.IsDefined(typeof(UriStorageType), token.URIStorageType))
            throw new ValidationException($"Invalid URI storage type: {token.URIStorageType}");
    }
}