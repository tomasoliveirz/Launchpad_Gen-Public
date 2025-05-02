namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.ScribanRenderingModels
{
    public class FunctionRenderingModel
    {
        public required string Name { get; set; }
        public string[] Parameters { get; set; } = [];
        public required string Body { get; set; }
        public string Visibility { get; set; } = "";
        public string Mutability { get; set; } = "";
    }
}
