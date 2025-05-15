using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Header;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels;

public class SolidityFileModel
{
    public required FileHeaderModel FileHeader { get; set; }
    public SolidityContractModel[] Contracts { get; set; } = [];
}
