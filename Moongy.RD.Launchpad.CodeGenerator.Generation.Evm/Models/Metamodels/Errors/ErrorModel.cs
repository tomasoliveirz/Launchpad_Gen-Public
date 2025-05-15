using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Base;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Parameters;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Errors;
public class ErrorModel : SolidityModel
{
    public required string Name { get; set; }
    public List<ErrorParameterModel> Parameters { get; set; } = [];
}
