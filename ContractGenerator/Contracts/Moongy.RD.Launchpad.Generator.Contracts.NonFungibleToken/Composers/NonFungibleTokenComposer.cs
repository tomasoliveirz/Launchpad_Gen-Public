
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels;
using Moongy.RD.Launchpad.Core.Enums;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Enumerables;
using Moongy.RD.Launchpad.Generator.Contracts.NonFungibleToken.Interfaces;
using Moongy.RD.Launchpad.Generator.Contracts.NonFungibleToken.Models;
using Moongy.RD.Launchpad.Generator.Contracts.NonFungibleToken.Validators;

namespace Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Composers
{
    public class NonFungibleTokenComposer : INonFungibleTokenComposer
    {
        public SolidityContractModel Compose(NonFungibleTokenModel tokenModel)
        {
            Validate(tokenModel);
            // TODO imcomplete
            var smartContractModel = new SolidityContractModel
            {
                Name = tokenModel.Name,
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
