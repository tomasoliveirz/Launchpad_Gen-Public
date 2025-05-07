using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels;
using Moongy.RD.Launchpad.Core.Enums;
using Moongy.RD.Launchpad.Generator.Contracts.SemiFungibleToken.Interfaces;
using Moongy.RD.Launchpad.Generator.Contracts.SemiFungibleToken.Models;
using Moongy.RD.Launchpad.Generator.Contracts.SemiFungibleToken.Validators;

namespace Moongy.RD.Launchpad.Generator.Contracts.SemiFungibleToken.Composers
{
    public class SemiFungibleTokenComposer : ISemiFungibleTokenComposer
    {
        public SolidityContractModel Compose(SemiFungibleTokenModel tokenModel)
        {
            Validate(tokenModel);

            var smartContractModel = new SolidityContractModel
            {
                Name = tokenModel.Name,
            };

            return smartContractModel;
        }

        public void Validate(SemiFungibleTokenModel tokenModel)
        {
            SemiFungibleTokenValidator _validator = new();
            _validator.Validate(tokenModel);
        }
    }
}
