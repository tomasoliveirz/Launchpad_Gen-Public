using Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.AdvancedFungibleToken.Models;
public class AdvancedFungibleTokenModel : FungibleTokenModel
{
    public Action<string, string, ulong>? OnTransferHook { get; set; } // Need to think about this
}
