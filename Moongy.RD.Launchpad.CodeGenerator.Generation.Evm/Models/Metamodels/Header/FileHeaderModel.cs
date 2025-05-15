using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Enums;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Header;
public class FileHeaderModel
{
    public SpdxLicense License { get; set; }
    public required VersionModel Version { get; set; }
}
