using Moongy.RD.Launchpad.ContractGenerator.Contracts.Core.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.Contracts.Core.Models;

namespace Moongy.RD.Launchpad.ContractGenerator.Contracts.FungibleToken.Models;
public class FungibleTokenModel : BaseTokenModel
{
    public String? Symbol { get; set; }
    public Byte Decimals { get; set; }
    public ulong Circulation { get; set; }
    public bool HasAutoSwap { get; set; }
    public bool HasFlashMint { get; set; }
    public ulong PremintAmmount { get; set; }
    public bool HasTokenRecovery { get; set; }
}
