using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Parameters;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Imports
{
    public class AbstractionImportModel : ImportModel
    {
        public required string Name { get; set; }
        public List<ConstructorParameterModel> ConstructorParameters { get; set; } = [];
    }
}
