using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Base;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.TypeReferences;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Structs;

public class StructPropertyModel : SolidityModel
{
    public required string Name { get; set; }
    public required TypeReference DataType { get; set; }
}
