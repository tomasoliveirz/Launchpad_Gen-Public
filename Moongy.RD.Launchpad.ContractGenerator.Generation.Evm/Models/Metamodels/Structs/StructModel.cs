using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Base;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Structs;
public class StructModel : SolidityModel
{
    public required string Name { get; set; }
    public StructPropertyModel[] Properties { get; set; } = [];
}