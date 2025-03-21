using Moongy.RD.Launchpad.ContractGenerator.Contracts.Core.Interfaces;

namespace Moongy.RD.Launchpad.ContractGenerator.Contracts.NonFungibleToken.Models;

public class NonFungibleTokenModel: IToken
{
    public enum UriStorageType
    {
        Centralized,
        Decentralized
    }

    public bool IsEnumerable { get; set; }
    public dictionary<ulong, string> TokenOwners { get; set; }
    public bool HasURI { get; set; }
    public string URI { get; set; }
    public UriStorageType URIStorageType { get; set; }
    public string URIStorageLocation { get; set; }
}
