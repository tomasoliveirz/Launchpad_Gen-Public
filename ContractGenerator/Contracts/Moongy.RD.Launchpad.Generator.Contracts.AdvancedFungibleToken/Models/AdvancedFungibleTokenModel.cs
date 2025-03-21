using Moongy.RD.Launchpad.ContractGenerator.Contracts.Core.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.Contracts.Core.Models;
using System;
namespace Moongy.RD.Launchpad.ContractGenerator.Contracts.AdvancedFungibleToken.Models;
public class AdvancedFungibleTokenModel : BaseTokenModel
{
    String? Symbol { get; set; }
    Byte Decimals { get; set; }
    ulong Circulation { get; set; }
    bool HasAutoSwap { get; set; }
    bool HasFlashMint { get; set; }
    ulong PremintAmmount { get; set; }
    bool HasTokenRecovery { get; set; }
    public Action<string, string, ulong>? OnTransferHook { get; set; } 
}
