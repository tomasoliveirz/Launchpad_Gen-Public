namespace Moongy.RD.Launchpad.CodeGenerator.Engine.Models
{
    public class ExtractedModels
    {
        public object Standard { get; set; } = null!;
        public List<object> Tokenomics { get; set; } = new();
        public List<object> Extensions { get; set; } = new();
    }
}