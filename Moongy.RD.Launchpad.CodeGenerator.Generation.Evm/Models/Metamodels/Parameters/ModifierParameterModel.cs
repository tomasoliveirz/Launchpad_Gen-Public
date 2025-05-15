using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Enums;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Parameters;

public class ModifierParameterModel : ParameterModel
{
    public SolidityMemoryLocation? Location { get; set; } = SolidityMemoryLocation.None;
}
