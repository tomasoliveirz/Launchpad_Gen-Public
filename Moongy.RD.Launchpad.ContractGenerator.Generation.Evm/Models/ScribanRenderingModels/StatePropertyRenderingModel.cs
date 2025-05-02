using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Base;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.ScribanRenderingModels
{
    public class StatePropertyRenderingModel
    {
        public required string Type { get; set; }
        public required string Name { get; set; }
        public required string Visibility { get; set; }
        public bool IsConstant { get; set; }
        public bool IsImmutable { get; set; }
        public bool HasInitialValue { get; set; }
        public string? InitialValue { get; set; }
    }
}