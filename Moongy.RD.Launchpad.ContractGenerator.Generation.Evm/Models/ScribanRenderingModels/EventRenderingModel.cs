namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.ScribanRenderingModels
{
    public class EventRenderingModel
    {
        public required string Name { get; set; }
        public string[] Parameters { get; set; } = [];
    }
}
