
using Moongy.RD.Launchpad.Core.Models.Metamodel;
using Moongy.RD.Launchpad.Generator.Contracts.AdvancedFungibleToken.Interfaces;
using Moongy.RD.Launchpad.Generator.Contracts.AdvancedFungibleToken.Models;
using Moongy.RD.Launchpad.Generator.Contracts.AdvancedFungibleToken.Validators;

namespace Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Composers
{
    public class AdvancedFungibleTokenComposer : IAdvancedFungibleTokenComposer
    {

        public SmartContractModel Compose(AdvancedFungibleTokenModel tokenModel)
        {
            Validate(tokenModel);

            var smartContractModel = new SmartContractModel
            {
                Name = tokenModel.Name
            };

            return smartContractModel;
        }

        public void Validate(AdvancedFungibleTokenModel tokenModel)
        {
            AdvancedFungibleTokenValidator _validator = new();
            _validator.Validate(tokenModel);
        }
    }
}
