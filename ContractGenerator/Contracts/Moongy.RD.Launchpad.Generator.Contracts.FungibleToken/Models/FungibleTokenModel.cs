using Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Models;
public class FungibleTokenModel : BaseTokenModel, IAutoSwappableToken, IDecimalToken, ITokenRecoverable
{
    public byte Decimals { get; set; }
    public ulong Circulation { get; set; }
    public bool HasAutoSwap { get; set; }
    public bool HasFlashMint { get; set; }
    public ulong PremintAmmount { get; set; }
    public bool HasTokenRecovery { get; set; }
}
