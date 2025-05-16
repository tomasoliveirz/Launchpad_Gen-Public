using Moongy.RD.Launchpad.CodeGenerator.Core.Attributes;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Interfaces;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Models;

public class NonFungibleTokenModel : BaseContractModel, IUriStorable
{
    [ContextProperty(Name = "isEnumerable", Type = PrimitiveType.Bool, Visibility = Visibility.Public, HasDefaultValue = true)]
    public bool IsEnumerable { get; set; }
    
    [ContextProperty(Name = "hasURI", Type = PrimitiveType.Bool, Visibility = Visibility.Public, HasDefaultValue = true)]
    public bool HasURI { get; set; }

    [ContextProperty(Name = "uri", Type = PrimitiveType.String, Visibility = Visibility.Public, HasDefaultValue = true)]
    public string? URI { get; set; }
    
    // como fazer com o type aqui?
    [ContextProperty(Name = "uriStorageType", Visibility = Visibility.Public, HasDefaultValue = true)]
    public UriStorageType URIStorageType { get; set; }

    [ContextProperty(Name = "uriStorageLocation", Type = PrimitiveType.String, Visibility = Visibility.Public, HasDefaultValue = true)]
    public string? URIStorageLocation { get; set; }
}