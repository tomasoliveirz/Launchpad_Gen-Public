using Moongy.RD.Launchpad.Generator.Contracts.Core.Enumerables;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.SemiFungibleToken.Models
{
    public class SemiFungibleTokenModel : BaseTokenModel, IUriStorable, IAutoSwappableToken
    {
        public byte Decimals { get; set; }
        public bool HasAutoSwap { get; set; }
        public bool HasSupplyControl { get; set; }
        public bool HasTokenRecovery { get; set; }
        public ulong MaxSupply { get; set; }
        public string? URI { get; set; }
        public bool HasURI { get; set; }
        public string? URIStorageLocation { get; set; }
        public UriStorageType URIStorageType { get; set; }

    }
}
