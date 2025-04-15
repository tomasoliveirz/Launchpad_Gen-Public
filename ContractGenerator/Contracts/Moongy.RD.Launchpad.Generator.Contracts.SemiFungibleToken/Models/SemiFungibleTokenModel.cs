using Moongy.RD.Launchpad.Core.Attributes;
using Moongy.RD.Launchpad.Core.Enums;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.SemiFungibleToken.Models
{
    [Token(Name = "Semi-Fungible Token", Tags = [TokenClassification.FungibleToken, TokenClassification.NonFugibleToken, TokenClassification.Multiple])]
    public class SemiFungibleTokenModel : BaseTokenModel, IAutoSwappableToken, IDecimalToken, ITokenRecoverable
    {
        public string? Symbol { get; set; }
        public byte Decimals { get; set; }
        public bool HasAutoSwap { get; set; }
        public bool HasSupplyControl { get; set; }
        public bool HasTokenRecovery { get; set; }
        public ulong MaxSupply { get; set; }
        public string? URI { get; set; }
        public bool HasURI { get; set; }

    }
}
