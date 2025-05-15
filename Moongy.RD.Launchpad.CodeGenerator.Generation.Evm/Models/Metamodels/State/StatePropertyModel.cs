using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Base;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.TypeReferences;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.State
{
    public class StatePropertyModel:SolidityModel
    {
        public required TypeReference Type { get; set; }
        public required string Name { get; set; }
        public SolidityVisibilityEnum Visibility { get; set; } = SolidityVisibilityEnum.Private;
        public string? InitialValue { get; set; }
        public bool IsConstant { get; set; } = false;
        public bool IsImmutable { get; set; } = false;
    }
}
