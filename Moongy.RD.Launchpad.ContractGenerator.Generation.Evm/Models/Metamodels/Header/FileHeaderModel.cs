using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.Core.Enums;
namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Header;
public class FileHeaderModel
{
    public SpdxLicense License { get; set; }
    public required VersionModel Version { get; set; }
}
