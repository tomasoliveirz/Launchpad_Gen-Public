using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Base;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.TypeReferences;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Parameters;

public abstract class ParameterModel : SolidityModel
{
    public required TypeReference Type { get; set; }
    public required string Name { get; set; }
    public int Index { get; set; } = int.MaxValue;
    public string? Value { get; set; }
}
