using Moongy.RD.Launchpad.Generator.Contracts.Core.Enumerables;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.NonFungibleToken.Models;

public class NonFungibleTokenModel : BaseTokenModel, IUriStorable
{
    public bool IsEnumerable { get; set; }
    public Dictionary<ulong, string> TokenOwners { get; set; }
    public bool HasURI { get; set; }
    public string? URI { get; set; }
    public UriStorageType URIStorageType { get; set; }
    public string? URIStorageLocation { get; set; }
}
