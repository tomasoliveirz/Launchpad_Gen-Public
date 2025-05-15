using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Base;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Version;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Header
{
    public class VersionModel :SolidityModel
    {
        public SoftwareVersion? Minimum {  get; set; }
        public SoftwareVersion? Maximum {  get; set; }
    }
}
