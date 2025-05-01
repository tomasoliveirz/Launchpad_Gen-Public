using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Base;
using Moongy.RD.Launchpad.Core.Models.Metamodel;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Header
{
    public class VersionModel :SolidityModel
    {
        public SoftwareVersion? Minimum {  get; set; }
        public SoftwareVersion? Maximum {  get; set; }
    }
}
