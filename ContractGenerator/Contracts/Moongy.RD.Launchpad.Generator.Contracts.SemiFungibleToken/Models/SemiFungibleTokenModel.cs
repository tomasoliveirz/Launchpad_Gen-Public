using Moongy.RD.Launchpad.Core.Attributes;
using Moongy.RD.Launchpad.Core.Enums;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.SemiFungibleToken.Models
{
    [Token(Name = "Semi-Fungible Token", Tags = [TokenClassification.FungibleToken, TokenClassification.NonFugibleToken, TokenClassification.Multiple])]
    public class SemiFungibleTokenModel : BaseTokenModel, IAutoSwappableToken, IDecimalToken, ITokenRecoverable
    {
        [MetaModelProperty(Name = nameof(Symbol), PropertyType = PropertyType.None, DataType = DataType.String)]
        public string? Symbol { get; set; }

        [MetaModelProperty(Name = nameof(Decimals), PropertyType = PropertyType.None, DataType = DataType.SmallInteger)]
        public byte Decimals { get; set; }

        [MetaModelProperty(Name = nameof(HasAutoSwap), PropertyType = PropertyType.None, DataType = DataType.Boolean)]
        public bool HasAutoSwap { get; set; }

        [MetaModelProperty(Name = nameof(HasSupplyControl), PropertyType = PropertyType.None, DataType = DataType.Boolean)]
        public bool HasSupplyControl { get; set; }

        [MetaModelProperty(Name = nameof(HasTokenRecovery), PropertyType = PropertyType.None, DataType = DataType.Boolean)]
        public bool HasTokenRecovery { get; set; }

        [MetaModelProperty(Name = nameof(MaxSupply), PropertyType = PropertyType.None, DataType = DataType.BigInteger)]
        public ulong MaxSupply { get; set; }

        [MetaModelProperty(Name = nameof(URI), PropertyType = PropertyType.None, DataType = DataType.String)]
        public string? URI { get; set; }

        [MetaModelProperty(Name = nameof(HasURI), PropertyType = PropertyType.None, DataType = DataType.Boolean)]
        public bool HasURI { get; set; }

    }
}
