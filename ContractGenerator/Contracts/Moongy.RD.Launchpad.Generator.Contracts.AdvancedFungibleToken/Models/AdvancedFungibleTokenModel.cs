using Moongy.RD.Launchpad.Core.Attributes;
using Moongy.RD.Launchpad.Core.Enums;
using Moongy.RD.Launchpad.Core.Models.Metamodel.Executions;
using Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.AdvancedFungibleToken.Models;

[Token(Name ="Advanced Fungible Token", Tags = [TokenClassification.FungibleToken]) ]
public class AdvancedFungibleTokenModel : FungibleTokenModel
{
    public List<Execution> PreTransferHooks { get; set; } = [];
    public List<Execution> PostTransferHooks { get; set; } = [];

}
