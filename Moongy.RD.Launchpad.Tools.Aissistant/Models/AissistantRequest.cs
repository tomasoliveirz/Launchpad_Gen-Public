using Moongy.RD.Launchpad.Tools.Aissistant.Enums;

namespace Moongy.RD.Launchpad.Tools.Aissistant.Models
{
    public class AissistantRequest
    {
        public string? Code { get; set; }
        public string? Version { get; set; }
        public string? Language { get; set; }
        public string? Description { get; set; }
        public OperationType Operation { get; set; }
    }
}
