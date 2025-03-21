using Moongy.RD.Launchpad.ContractGenerator.Contracts.Core.Validators;
using Moongy.RD.Launchpad.ContractGenerator.Contracts.FungibleToken.Models;

namespace Moongy.RD.Launchpad.ContractGenerator.Contracts.FungibleToken.Validators;

   public class FungibleTokenValidator : BaseTokenValidator<FungibleTokenModel>
    {
        protected override void ValidateSpecific(FungibleTokenModel token)
        {
            if (string.IsNullOrWhiteSpace(token.Symbol))
                throw new ArgumentException("Token symbol cannot be empty for a fungible token.");
            if (token.Symbol.Length < 2 || token.Symbol.Length > 5)
                throw new ArgumentException("Token symbol must have between 2 and 5 characters.");

            if (token.Decimals < 0 || token.Decimals > 18)
                throw new ArgumentException("Token decimals must be between 0 and 18.");
        }
    }