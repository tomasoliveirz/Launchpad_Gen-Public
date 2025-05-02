using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Header;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels;

public class SolidityFile
{
    public required FileHeaderModel FileHeader { get; set; }
    public SolidityContractModel[] Contracts { get; set; } = [];
}
