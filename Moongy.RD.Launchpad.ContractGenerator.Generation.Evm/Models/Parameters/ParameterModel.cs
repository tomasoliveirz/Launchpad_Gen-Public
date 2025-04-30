using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Base;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.TypeReferences;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Parameters;

public class ParameterModel : SolidityModel
{
    public required TypeReference Type { get; set; }
    public required string Name { get; set; }
}
