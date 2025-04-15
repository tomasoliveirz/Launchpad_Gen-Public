
using Moongy.RD.Launchpad.Core.Models.Metamodel;
using Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Interfaces;
using Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Models;
using Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Validators;

namespace Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Composers
{
    public class FungibleTokenComposer : IFungibleTokenComposer
    {

        public SmartContractModel Compose(FungibleTokenModel tokenModel)
        {
            Validate(tokenModel);

            var smartContractModel = new SmartContractModel
            {
                Name = tokenModel.Name
            };

            return smartContractModel;
        }

        public void Validate(FungibleTokenModel tokenModel)
        {
            FungibleTokenValidator _validator = new();
            _validator.Validate(tokenModel);
        }
    }
}
