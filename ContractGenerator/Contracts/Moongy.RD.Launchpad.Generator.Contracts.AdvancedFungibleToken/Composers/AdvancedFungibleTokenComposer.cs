using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels;
using Moongy.RD.Launchpad.Generator.Contracts.AdvancedFungibleToken.Interfaces;
using Moongy.RD.Launchpad.Generator.Contracts.AdvancedFungibleToken.Models;
using Moongy.RD.Launchpad.Generator.Contracts.AdvancedFungibleToken.Validators;
using Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Composers;


namespace Moongy.RD.Launchpad.Generator.Contracts.AdvancedFungibleToken.Composers
{
    public class AdvancedFungibleTokenComposer : FungibleTokenComposer, IAdvancedFungibleTokenComposer
    {
        public SolidityContractModel Compose(AdvancedFungibleTokenModel tokenModel)
        {
            Validate(tokenModel);

            // TODO add the logic to compose the advanced fungible token model
            var smartContractModel = new SolidityContractModel
            {
                Name = tokenModel.Name,
            };

            // TODO: Add the logic to compose the advanced fungible token model
            if (tokenModel.PreTransferHooks != null && tokenModel.PreTransferHooks.Any())
            {

            }

            // TODO: Add the logic to compose the advanced fungible token model
            if (tokenModel.PostTransferHooks != null && tokenModel.PostTransferHooks.Any())
            {

            }

            return smartContractModel;
        }

        public void Validate(AdvancedFungibleTokenModel tokenModel)
        {
            AdvancedFungibleTokenValidator _validator = new();
            _validator.Validate(tokenModel);
        }

    }
}
