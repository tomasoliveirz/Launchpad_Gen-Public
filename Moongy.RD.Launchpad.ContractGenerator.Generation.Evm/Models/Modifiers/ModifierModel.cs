using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Base;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Parameters;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Modifiers;
public class ModifierModel : SolidityModel
{
    public required string Name { get; set; }
    public List<ParameterModel> Parameters { get; set; } = [];

    // TODO: This can be improved if necessary
    public List<string> Arguments { get; set; } = [];

    // TODO: This can be improved with modeling
    public required string Body { get; set; }
}