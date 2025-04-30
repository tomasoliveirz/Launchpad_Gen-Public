using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Base;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Parameters;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Errors;
public class ErrorModel : SolidityModel
{
    public required string Name { get; set; }
    public List<ErrorParameterModel> Parameters { get; set; } = new();
}
