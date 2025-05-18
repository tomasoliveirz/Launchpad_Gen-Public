using Moongy.RD.Launchpad.CodeGenerator.Core.Attributes;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Interfaces;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Models;

public class NonFungibleTokenModel : BaseContractModel, IUriStorable
{
    public bool IsEnumerable { get; set; }
    
    public bool HasURI { get; set; }

    public string? URI { get; set; }
    
    public UriStorageType URIStorageType { get; set; }

    public string? URIStorageLocation { get; set; }
}