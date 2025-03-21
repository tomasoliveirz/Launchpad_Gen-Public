using Moongy.RD.Launchpad.ContractGenerator.Contracts.Core.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.Contracts.Core.Models;

namespace Moongy.RD.Launchpad.ContractGenerator.Contracts.FungibleToken.Models;
public class FungibleTokenModel : BaseTokenModel
{
    String? Symbol { get; set; }
    Byte Decimals { get; set; }
    ulong Circulation { get; set; }
    bool HasAutoSwap { get; set; }
    bool HasFlashMint { get; set; }
    ulong PremintAmmount { get; set; }
    bool HasTokenRecovery { get; set; }
}
