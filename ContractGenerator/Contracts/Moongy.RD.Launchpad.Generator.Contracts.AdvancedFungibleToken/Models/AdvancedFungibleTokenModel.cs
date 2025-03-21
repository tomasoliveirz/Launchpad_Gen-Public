using Moongy.RD.Launchpad.Generator.Contracts.Core.Models;
using System;
namespace Moongy.RD.Launchpad.Generator.Contracts.AdvanceFungibleToken.Models;
public class AdvancedFungibleTokenModel : BaseTokenModel
{
    string? Symbol { get; set; }
    byte Decimals { get; set; }
    ulong Circulation { get; set; }
    bool HasAutoSwap { get; set; }
    bool HasFlashMint { get; set; }
    ulong PreMintAmmount { get; set; }
    bool HasTokenRecovery { get; set; }
    public Action<string, string, ulong>? OnTransferHook { get; set; }
}
