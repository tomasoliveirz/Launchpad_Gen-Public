using Moongy.RD.Launchpad.Core.Attributes;
using Moongy.RD.Launchpad.Core.Enums;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Enumerables;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.NonFungibleToken.Models;

[Token(Name = "Non-Fungible Token", Tags = [TokenClassification.NonFugibleToken, TokenClassification.Collectables])]
public class NonFungibleTokenModel : BaseTokenModel, IUriStorable
{
    [MetaModelProperty(Name = nameof(IsEnumerable), PropertyType = PropertyType.None, DataType = DataType.Boolean)]
    public bool IsEnumerable { get; set; }

    [MetaModelProperty(Name = nameof(HasURI), PropertyType = PropertyType.None, DataType = DataType.Boolean)]
    public bool HasURI { get; set; }

    [MetaModelProperty(Name = nameof(URI), PropertyType = PropertyType.None, DataType = DataType.String)]
    public string? URI { get; set; }

    [MetaModelProperty(Name = nameof(URIStorageType), PropertyType = PropertyType.None, DataType = DataType.Enum)]
    public UriStorageType URIStorageType { get; set; }

    [MetaModelProperty(Name = nameof(URIStorageLocation), PropertyType = PropertyType.None, DataType = DataType.String)]
    public string? URIStorageLocation { get; set; }
}
