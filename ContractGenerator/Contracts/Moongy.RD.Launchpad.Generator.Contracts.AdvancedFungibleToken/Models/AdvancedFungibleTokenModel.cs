using Moongy.RD.Launchpad.ContractGenerator.Contracts.Core.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.Contracts.Core.Models;
using Moongy.RD.Launchpad.ContractGenerator.Contracts.FungibleToken.Models;
using System;
namespace Moongy.RD.Launchpad.ContractGenerator.Contracts.AdvancedFungibleToken.Models;
public class AdvancedFungibleTokenModel : FungibleTokenModel
{
    public Action<string, string, ulong>? OnTransferHook { get; set; } // Need to think about this
}
