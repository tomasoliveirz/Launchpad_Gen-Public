
using Moongy.RD.Launchpad.Core.Enums;
using Moongy.RD.Launchpad.Core.Models.Metamodel;
using Moongy.RD.Launchpad.Core.Models.Metamodel.Base;
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
                        Name = "HasAutoSwap",
                        Value = tokenModel.HasAutoSwap.ToString().ToLower(),
                        PropertyType = PropertyType.None
                    },
                    new PrimitiveProperty
                    {
                        Name = "HasSupplyControl",
                        Value = tokenModel.HasSupplyControl.ToString().ToLower(),
                        PropertyType = PropertyType.None
                    },
                    new PrimitiveProperty
                    {
                        Name = "HasTokenRecovery",
                        Value = tokenModel.HasTokenRecovery.ToString().ToLower(),
                        PropertyType = PropertyType.None
                    },
                    new PrimitiveProperty
                    {
                        Name = "MaxSupply",
                        Value = tokenModel.MaxSupply.ToString(),
                        PropertyType = PropertyType.None
                    },
                    new PrimitiveProperty
                    {
                        Name = "HasURI",
                        Value = tokenModel.HasURI.ToString().ToLower(),
                        PropertyType = PropertyType.None
                    },
                    new PrimitiveProperty
                    {
                        Name = "BaseURI",
                        Value = tokenModel.HasURI && !string.IsNullOrEmpty(tokenModel.URI) ? $"\"{tokenModel.URI}\"" : "\"\"",
                        PropertyType = PropertyType.None
                    }
                }
            };

            return smartContractModel;
        }

        public void Validate(SemiFungibleTokenModel tokenModel)
        {
            SemiFungibleTokenValidator _validator = new();
            _validator.Validate(tokenModel);
        }
    }
    public class PrimitiveProperty : ContractProperty
    {
        public string? Value { get; set; }
    }
}
