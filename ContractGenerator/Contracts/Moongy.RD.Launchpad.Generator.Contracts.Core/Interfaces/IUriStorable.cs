using Moongy.RD.Launchpad.Generator.Contracts.Core.Enumerables;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces
{
    public interface IUriStorable
    {
        string? URI { get; set; }
        string? URIStorageLocation { get; set; }
        UriStorageType URIStorageType { get; set; }
    }
}
