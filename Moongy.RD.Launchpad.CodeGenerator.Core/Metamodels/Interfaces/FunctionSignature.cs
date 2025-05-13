using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Interfaces
{
    public class FunctionSignature
    {
        public required string Name { get; set; }
        public List<ParameterDefinition> Parameters { get; set; } = [];
        public List<ParameterDefinition> ReturnParameters { get; set; } = [];
    }
}
