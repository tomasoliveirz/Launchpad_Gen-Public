using Moongy.RD.Launchpad.Core.Attributes;
using Moongy.RD.Launchpad.Core.Enums;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Enumerables;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.NonFungibleToken.Models;

[Token(Name = "Non-Fungible Token", Tags = [TokenClassification.NonFugibleToken, TokenClassification.Collectables])]
public class NonFungibleTokenModel : BaseTokenModel, IUriStorable
{
    public bool IsEnumerable { get; set; }
    public bool HasURI { get; set; }
    public string? URI { get; set; }
    public UriStorageType URIStorageType { get; set; }
    public string? URIStorageLocation { get; set; }
}
