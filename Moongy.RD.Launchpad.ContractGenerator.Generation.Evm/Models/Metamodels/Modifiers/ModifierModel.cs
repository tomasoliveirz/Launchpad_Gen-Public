using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Base;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Parameters;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Modifiers;
public class ModifierModel : SolidityModel
{
    public required string Name { get; set; }
    public List<ModifierParameterModel> Parameters { get; set; } = [];

    // TODO: This can be improved if necessary
    public List<string> Arguments { get; set; } = [];

    // TODO: This can be improved with modeling
    public required string Body { get; set; }
}