
using Moongy.RD.Launchpad.Core.Enums;
using Moongy.RD.Launchpad.Core.Models.Metamodel;
using Moongy.RD.Launchpad.Core.Models.Metamodel.Base;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Enumerables;
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
                Name = tokenModel.Name,
                Properties = new List<ContractProperty>
                {
                    new PrimitiveProperty
                    {
                        Name = "IsEnumerable",
                        Value = tokenModel.IsEnumerable.ToString().ToLower(),
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
                    },
                    new PrimitiveProperty
                    {
                        Name = "URIStorageType",
                        Value = $"{(int)tokenModel.URIStorageType}",
                        PropertyType = PropertyType.None 
                    },
                    new PrimitiveProperty
                    {
                        Name = "URIStorageLocation",
                        Value = tokenModel.URIStorageType != UriStorageType.Centralized && !string.IsNullOrEmpty(tokenModel.URIStorageLocation) ? $"\"{tokenModel.URIStorageLocation}\"" : "\"\"",
                        PropertyType = PropertyType.None 
                    }
                }
            };


            return smartContractModel;
        }

        public void Validate(NonFungibleTokenModel tokenModel)
        {
            NonFungibleTokenValidator _validator = new();
            _validator.Validate(tokenModel);
        }
    }
    public class PrimitiveProperty : ContractProperty
    {
        public string? Value { get; set; }
    }
}
