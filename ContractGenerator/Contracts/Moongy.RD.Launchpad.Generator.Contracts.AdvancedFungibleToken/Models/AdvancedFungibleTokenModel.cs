using Moongy.RD.Launchpad.Core.Attributes;
using Moongy.RD.Launchpad.Core.Enums;
using Moongy.RD.Launchpad.Core.Models.Metamodel.Executions;
using Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.AdvancedFungibleToken.Models;

[Token(Name ="Advanced Fungible Token", Tags = [TokenClassification.FungibleToken]) ]
public class AdvancedFungibleTokenModel : FungibleTokenModel
{
    [MetaModelProperty(Name = nameof(PreTransferHooks), PropertyType = PropertyType.None, DataType = DataType.Array)]
    public List<Execution> PreTransferHooks { get; set; } = [];

    [MetaModelProperty(Name = nameof(PostTransferHooks), PropertyType = PropertyType.None, DataType = DataType.Array)]
    public List<Execution> PostTransferHooks { get; set; } = [];

}
