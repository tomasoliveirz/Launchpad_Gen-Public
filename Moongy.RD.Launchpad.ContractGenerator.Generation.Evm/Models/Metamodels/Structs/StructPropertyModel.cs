using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Base;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.TypeReferences;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Structs;

public class StructPropertyModel : SolidityModel
{
    public required string Name { get; set; }
    public required TypeReference DataType { get; set; }
}
