using Moongy.RD.Launchpad.Core.Exceptions;

using Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;
using Moongy.RD.Launchpad.Generator.Contracts.SemiFungibleToken.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.SemiFungibleToken.Validators;

public class SemiFungibleTokenValidator : BaseTokenValidator<SemiFungibleTokenModel>
{
    public override void Validate(SemiFungibleTokenModel token)
    {
        
        /*
         semi-fungible token validation
          - auto-swap
          - decimals
          - supply control
          - token recovery
          - symbol
          - uri  
         */
        base.Validate(token);
        AutoSwapValidator.Validate(token);
        DecimalsValidator.Validate(token);
        SupplyControlValidator.Validate(token);
        TokenRecoveryValidator.Validate(token);
        SymbolValidator.Validate(token.Symbol, true, "semi-fungible token");
        if (token.HasURI)
            URIValidator.Validate(token.URI, true);
        
    }
}

