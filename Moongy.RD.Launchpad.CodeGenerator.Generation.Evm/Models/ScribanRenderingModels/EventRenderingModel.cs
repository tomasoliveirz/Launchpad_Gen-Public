namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.ScribanRenderingModels
{
    public class EventRenderingModel
    {
        public required string Name { get; set; }
        public string[] Parameters { get; set; } = [];
    }
}
