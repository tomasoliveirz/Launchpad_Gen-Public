using Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;
using Moongy.RD.Launchpad.Generator.Contracts.SemiFungibleToken.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.SemiFungibleToken.Validators;

public class SemiFungibleTokenValidator : BaseTokenValidator<SemiFungibleTokenModel>
{

    protected override void ValidateSpecific(SemiFungibleTokenModel token)
    {
        base.ValidateTokenomics(token);

        if (string.IsNullOrWhiteSpace(token.Symbol))
            throw new ArgumentException("Token symbol cannot be empty for a semi-fungible token.");
        if (token.Symbol.Length < 2 || token.Symbol.Length > 5)
            throw new ArgumentException("Token symbol must have between 2 and 5 characters for a semi-fungible token.");

        if (token.Decimals < 0 || token.Decimals > 18)
            throw new ArgumentException("Token decimals must be between 0 and 18 for a semi-fungible token.");

        if (token.HasSupplyControl && token.MaxSupply == 0)
            throw new ArgumentException("MaxSupply must be greater than 0 if supply control is enabled.");

        if (string.IsNullOrWhiteSpace(token.URI))
            throw new ArgumentException("URI cannot be empty for a semi-fungible token.");

    }
}