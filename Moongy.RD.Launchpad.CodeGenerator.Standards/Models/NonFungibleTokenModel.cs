using Moongy.RD.Launchpad.CodeGenerator.Standards.Enums;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Models;

public class NonFungibleTokenModel : BaseContractModel
{
    public bool IsEnumerable { get; set; }

    public bool HasURI { get; set; }

    public string? URI { get; set; }

    public UriStorageType URIStorageType { get; set; }

    public string? URIStorageLocation { get; set; }
}
