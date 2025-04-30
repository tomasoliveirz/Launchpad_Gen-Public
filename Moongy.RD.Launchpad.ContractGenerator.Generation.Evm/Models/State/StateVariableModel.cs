using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Base;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.TypeReferences;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.State
{
    public class StateVariableModel:SolidityModel
    {
        public required TypeReference Type { get; set; }
        public required string Name { get; set; }
        public SolidityVisibilityEnum Visibility { get; set; } = SolidityVisibilityEnum.Private;
        public string? InitialValue { get; set; }
        public bool IsConstant { get; set; } = false;
        public bool IsImmutable { get; set; } = false;
    }
}
