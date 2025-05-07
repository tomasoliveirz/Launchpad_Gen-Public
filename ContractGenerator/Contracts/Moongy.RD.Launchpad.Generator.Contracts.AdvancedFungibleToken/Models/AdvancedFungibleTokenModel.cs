using Moongy.RD.Launchpad.Core.Attributes;
using Moongy.RD.Launchpad.Core.Enums;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.AdvancedFungibleToken.Models;

[Token(Name ="Advanced Fungible Token", Tags = [TokenClassification.FungibleToken])]
public class AdvancedFungibleTokenModel : BaseTokenModel, IAutoSwappableToken, IDecimalToken, ITokenRecoverable
{
    [MetaModelProperty(Name = nameof(PreTransferHooks), PropertyType = PropertyType.None, DataType = DataType.Array)]
    // TODO Type of list placeholder
    public List<string> PreTransferHooks { get; set; } = [];

    [MetaModelProperty(Name = nameof(PostTransferHooks), PropertyType = PropertyType.None, DataType = DataType.Array)]
    // TODO  Type of list placeholder
    public List<string> PostTransferHooks { get; set; } = [];
    [MetaModelProperty(Name = nameof(Symbol), PropertyType = PropertyType.None, DataType = DataType.String)]
    public string? Symbol { get; set; }

    [MetaModelProperty(Name = nameof(Decimals), PropertyType = PropertyType.None, DataType = DataType.Integer)]
    public byte Decimals { get; set; }

    [MetaModelProperty(Name = nameof(Circulation), PropertyType = PropertyType.None, DataType = DataType.BigInteger)]
    public ulong Circulation { get; set; }

    [MetaModelProperty(Name = nameof(HasAutoSwap), PropertyType = PropertyType.None, DataType = DataType.Boolean)]
    public bool HasAutoSwap { get; set; }

    [MetaModelProperty(Name = nameof(HasFlashMint), PropertyType = PropertyType.None, DataType = DataType.Boolean)]
    public bool HasFlashMint { get; set; }

    [MetaModelProperty(Name = nameof(PremintAmount), PropertyType = PropertyType.None, DataType = DataType.BigInteger)]
    public ulong PremintAmount { get; set; }

    [MetaModelProperty(Name = nameof(HasTokenRecovery), PropertyType = PropertyType.None, DataType = DataType.Boolean)]
    public bool HasTokenRecovery { get; set; }
}
