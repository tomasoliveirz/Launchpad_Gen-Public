
using Moongy.RD.Launchpad.Core.Models.Metamodel;
using Moongy.RD.Launchpad.Generator.Contracts.SemiFungibleToken.Interfaces;
using Moongy.RD.Launchpad.Generator.Contracts.SemiFungibleToken.Models;
using Moongy.RD.Launchpad.Generator.Contracts.SemiFungibleToken.Validators;

namespace Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Composers
{
    public class SemiFungibleTokenComposer : ISemiFungibleTokenComposer
    {

        public SmartContractModel Compose(SemiFungibleTokenModel tokenModel)
        {
            Validate(tokenModel);

            var smartContractModel = new SmartContractModel
            {
                Name = tokenModel.Name
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
