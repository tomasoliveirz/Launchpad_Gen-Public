using Moongy.RD.Launchpad.CodeGenerator.Core.Attributes;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Models
{
    public class SemiFungibleTokenModel : BaseContractModel
    {
        // Does this really have the symbol ?
        [Required(Name = "Symbol")]
        [ContextProperty(Name = "symbol", Type = PrimitiveType.String, Visibility = Visibility.Public, HasDefaultValue = true)]
        public string? Symbol { get; set; }

        [ContextProperty(Name = "decimals", Type = PrimitiveType.String, Visibility = Visibility.Public, HasDefaultValue = true)]
        public byte Decimals { get; set; } = 18;
        [ContextProperty(Name = "hasDecimals", Type = PrimitiveType.Bool, Visibility = Visibility.Public, HasDefaultValue = false)]
        public bool HasAutoSwap { get; set; }
        [ContextProperty(Name = "hasSupplyControl", Type = PrimitiveType.Bool, Visibility = Visibility.Public, HasDefaultValue = false)]
        public bool HasSupplyControl { get; set; }

        [ContextProperty(Name = "hasRecovery", Type = PrimitiveType.Bool, Visibility = Visibility.Public, HasDefaultValue = false)]
        public bool HasTokenRecovery { get; set; }

        [ContextProperty(Name = "max_supply", Type = PrimitiveType.Uint256, Visibility = Visibility.Public, HasDefaultValue = true)]
        public ulong MaxSupply { get; set; } = 0;
        [ContextProperty(Name = "uri", Type = PrimitiveType.String, Visibility = Visibility.Public, HasDefaultValue = false)]
        public string? URI { get; set; }
        [ContextProperty(Name = "hasUri", Type = PrimitiveType.Bool, Visibility = Visibility.Public, HasDefaultValue = false)]
        public bool HasURI { get; set; }
    }
}
