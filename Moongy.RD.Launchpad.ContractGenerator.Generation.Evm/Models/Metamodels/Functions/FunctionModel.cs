using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Base;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Modifiers;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Parameters;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Functions;
public class FunctionModel : SolidityModel
{
    public required string Name { get; set; }
    public List<FunctionParameterModel> Parameters { get; set; } = [];
    public List<ReturnParameterModel> ReturnParameters { get; set; } = [];
    public SolidityVisibilityEnum Visibility { get; set; } = SolidityVisibilityEnum.Public;
    public SolidityFunctionMutabilityEnum Mutability { get; set; } = SolidityFunctionMutabilityEnum.None;
    public List<ModifierModel> Modifiers { get; set; } = [];

    // TODO: Can be improved with models
    public required string Body { get; set; }
    public bool IsConstructor { get; set; } = false;
    public bool IsVirtual { get; set; } = false;
    public bool IsOverride { get; set; } = false;
}
