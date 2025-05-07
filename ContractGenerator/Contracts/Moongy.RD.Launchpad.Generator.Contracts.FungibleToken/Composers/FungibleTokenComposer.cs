
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels;
using Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Interfaces;
using Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Models;
using Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Validators;

namespace Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Composers
{
    public class FungibleTokenComposer : IFungibleTokenComposer
    {

        public SolidityContractModel Compose(FungibleTokenModel tokenModel)
        {
            Validate(tokenModel);
            // TODO imcomplete
            var smartContractModel = new SolidityContractModel
            {
                Name = tokenModel.Name,
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
