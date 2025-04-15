using Moongy.RD.Launchpad.Core.Attributes;
using Moongy.RD.Launchpad.Core.Enums;
using Moongy.RD.Launchpad.Core.Models.Metamodel;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Models;

public class MetaModelPropertyAttribute : Attribute
{
    public string? Name { get; set; }
}

[Token(Name = "Fungible Token", Tags = [TokenClassification.FungibleToken])]
public class FungibleTokenModel : BaseTokenModel, IAutoSwappableToken, IDecimalToken, ITokenRecoverable
{
    [MetaModelProperty(Name = nameof(SmartContractModel.Name))]
    public string? Symbol { get; set; }
    public byte Decimals { get; set; }
    public ulong Circulation { get; set; }
    public bool HasAutoSwap { get; set; }
    public bool HasFlashMint { get; set; }
    public ulong PremintAmmount { get; set; }
    public bool HasTokenRecovery { get; set; }
}
