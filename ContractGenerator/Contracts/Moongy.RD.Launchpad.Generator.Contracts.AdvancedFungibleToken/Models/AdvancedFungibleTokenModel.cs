using Moongy.RD.Launchpad.Core.Models.Metamodel.Executions;
using Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.AdvancedFungibleToken.Models;
public class AdvancedFungibleTokenModel : FungibleTokenModel
{
    public List<Execution> PreTransferHooks { get; set; } = [];
    public List<Execution> PostTransferHooks { get; set; } = [];

}
