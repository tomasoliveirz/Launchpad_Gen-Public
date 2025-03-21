using Moongy.RD.Launchpad.Generator.Contracts.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.NonFungibleToken.Models;

public class NonFungibleTokenModel : BaseTokenModel
{
    public enum UriStorageType
    {
        Centralized,
        Decentralized
    }

    public bool IsEnumerable { get; set; }
    public Dictionary<ulong, string> TokenOwners { get; set; }
    public bool HasURI { get; set; }
    public string URI { get; set; }
    public UriStorageType URIStorageType { get; set; }
    public string URIStorageLocation { get; set; }
}
