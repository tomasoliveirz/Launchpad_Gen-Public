using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Base;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Parameters;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Modifiers;
public class ModifierModel : SolidityModel
{
    public required string Name { get; set; }
    public List<ModifierParameterModel> Parameters { get; set; } = [];
    public List<string> Arguments { get; set; } = [];
    public required string Body { get; set; }
}