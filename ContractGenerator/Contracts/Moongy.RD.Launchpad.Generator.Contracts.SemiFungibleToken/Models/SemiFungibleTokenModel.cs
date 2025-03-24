using Moongy.RD.Launchpad.ContractGenerator.Contracts.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.SemiFungibleToken.Models
{
    public class SemiFungibleTokenModel : BaseTokenModel
    {
        public string Symbol { get; set; }
        public byte Decimals { get; set; }
        public bool HasAutoSwap { get; set; }
        public bool HasSupplyControl { get; set; }
        public bool HasTokenRecovery { get; set; }
        public ulong MaxSupply { get; set; }
        public string URI { get; set; }
    }
}
