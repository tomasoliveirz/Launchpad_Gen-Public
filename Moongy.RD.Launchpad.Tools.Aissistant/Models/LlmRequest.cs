using Moongy.RD.Launchpad.Tools.Aissistant.Enums;

namespace Moongy.RD.Launchpad.Tools.Aissistant.Models;

public class LlmRequest
{
    public required string Model { get; set; }
    public List<LlmMessage> Messages { get; set; } = new();
    public double Temperature { get; set; } = 0.7;
}
