using Moongy.RD.Launchpad.CodeGenerator.Standards.Enums;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Interfaces
{
    public interface IUriStorable
    {
        string? URI { get; }
        string? URIStorageLocation { get; }
        UriStorageType URIStorageType { get; }
    }
}