using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;
using Moongy.RD.Launchpad.Generator.Contracts.NonFungibleToken.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.NonFungibleToken.Validators;

public class NonFungibleTokenValidator : BaseTokenValidator<NonFungibleTokenModel>
{
    public override void Validate(NonFungibleTokenModel token)
    {
        /*
         non fungible token validations
         - enumerable
         - uri
         - uri storage
        */
        base.Validate(token);

        TokenOwnersValidator.Validate(token); // enumerable
        if (token.HasURI)
        {
            URIValidator.Validate(token.URI, true);
            UriStorageValidator.Validate(token);
        }

    }
}
