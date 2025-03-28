using Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.AdvancedFungibleToken.Models;
public class AdvancedFungibleTokenModel : FungibleTokenModel
{
    public List<Func<string, string, ulong, bool>> PreTransferHooks { get; set; } = new List<Func<string, string, ulong, bool>>();
    public List<Action<string, string, ulong>> PostTransferHooks { get; set; } = new List<Action<string, string, ulong>>();
}
