using Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;
using Moongy.RD.Launchpad.Generator.Contracts.NonFungibleToken.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.NonFungibleToken.Validators;

public class NonFungibleTokenValidator : BaseTokenValidator<NonFungibleTokenModel>
{

    //protected override void ValidateSpecific(NonFungibleTokenModel token)
    //{

    //    if (token.IsEnumerable && (token.TokenOwners == null || token.TokenOwners.Count == 0))
    //    {
    //        throw new ArgumentException("Token is enumerable but no TokenOwners mapping provided.");
    //    }

    //    if (token.HasURI)
    //    {
    //        if (string.IsNullOrWhiteSpace(token.URI))
    //            throw new ArgumentException("HasURI = true, but URI is null or empty.");

    //        if (string.IsNullOrWhiteSpace(token.URIStorageLocation))
    //            throw new ArgumentException("HasURI = true, but URIStorageLocation is null or empty.");
    //    }


    //}
}