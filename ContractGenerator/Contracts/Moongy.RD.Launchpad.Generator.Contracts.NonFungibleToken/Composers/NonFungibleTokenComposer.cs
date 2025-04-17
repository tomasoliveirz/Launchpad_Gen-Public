
using Moongy.RD.Launchpad.Core.Models.Metamodel;
using Moongy.RD.Launchpad.Generator.Contracts.NonFungibleToken.Interfaces;
using Moongy.RD.Launchpad.Generator.Contracts.NonFungibleToken.Models;
using Moongy.RD.Launchpad.Generator.Contracts.NonFungibleToken.Validators;

namespace Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Composers
{
    public class NonFungibleTokenComposer : INonFungibleTokenComposer
    {

        public SmartContractModel Compose(NonFungibleTokenModel tokenModel)
        {
            Validate(tokenModel);

            var smartContractModel = new SmartContractModel
            {
                Name = tokenModel.Name
            };

            return smartContractModel;
        }

        public void Validate(NonFungibleTokenModel tokenModel)
        {
            NonFungibleTokenValidator _validator = new();
            _validator.Validate(tokenModel);
        }
    }
}
