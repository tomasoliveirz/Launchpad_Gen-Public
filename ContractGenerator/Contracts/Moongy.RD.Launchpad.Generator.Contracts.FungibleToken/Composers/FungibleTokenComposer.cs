
using Moongy.RD.Launchpad.Core.Enums;
using Moongy.RD.Launchpad.Core.Models.Metamodel;
using Moongy.RD.Launchpad.Core.Models.Metamodel.Base;
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
                Name = tokenModel.Name,
                Properties = new List<ContractProperty>
                {
                    new PrimitiveProperty
                    {
                        Name = "Symbol",
                        Value = $"\"{tokenModel.Symbol}\"",
                        PropertyType = PropertyType.None
                    },
                    new PrimitiveProperty
                    {
                        Name = "Decimals",
                        Value = tokenModel.Decimals.ToString(),
                        PropertyType = PropertyType.None
                    },
                    new PrimitiveProperty
                    {
                        Name = "Circulation",
                        Value = tokenModel.Circulation.ToString(),
                        PropertyType = PropertyType.None
                    },
                    new PrimitiveProperty
                    {
                        Name = "HasAutoSwap",
                        Value = tokenModel.HasAutoSwap.ToString().ToLower(),
                        PropertyType = PropertyType.None
                    },
                    new PrimitiveProperty
                    {
                        Name = "HasFlashMint",
                        Value = tokenModel.HasFlashMint.ToString().ToLower(),
                        PropertyType = PropertyType.None
                    },
                    new PrimitiveProperty
                    {
                        Name = "PremintAmount",
                        Value = tokenModel.PremintAmmount.ToString(),
                        PropertyType = PropertyType.None
                    },
                    new PrimitiveProperty
                    {
                        Name = "HasTokenRecovery",
                        Value = tokenModel.HasTokenRecovery.ToString().ToLower(),
                        PropertyType = PropertyType.None
                    }
                }
            };

            return smartContractModel;
        }

        public void Validate(FungibleTokenModel tokenModel)
        {
            FungibleTokenValidator _validator = new();
            _validator.Validate(tokenModel);
        }
    }
    public class PrimitiveProperty : ContractProperty
    {
        public string? Value { get; set; }
    }
}
