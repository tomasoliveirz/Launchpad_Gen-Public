namespace Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others
{
    public class TriggerDefinition
    {
        public required string Name { get; set; }
        public List<ParameterDefinition> Parameters { get; set; } = [];
        public TriggerKind Kind { get; set; }
    }
}
