using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;
using Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Validators;

public class FungibleTokenValidator : BaseTokenValidator<FungibleTokenModel>
{
    public override void Validate(FungibleTokenModel token)
    {
        /*
         fungible token validations
         - auto swap
         - ciurculation
         - decimals 
         - flash mint
         - premint
         - token recovery
         - symbol   
         */
        base.Validate(token);
        
        AutoSwapValidator.Validate(token);
        CirculationValidator.Validate(token);
        DecimalsValidator.Validate(token);
        FlashMintValidator.Validate(token);
        PremintValidator.Validate(token);
        TokenRecoveryValidator.Validate(token);
        SymbolValidator.Validate(token.Symbol, true, "fungible token");
    }
}