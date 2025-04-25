using Moongy.RD.Launchpad.Tools.Aissistant.Enums;

namespace Moongy.RD.Launchpad.Tools.Aissistant;

public class AissistantOptions
{
    public List<LlmConfiguration> Models { get; set; } = [];
    public LlmMode? DefaultMode { get; set; }
}

public class LlmConfiguration
{
    public LlmMode? DefaultMode { get; set; }
    public LlmModel Model { get; set; }
    public required string Endpoint { get; set; }
    public string? Token { get; set; }
    public string? Secret { get; set; }
}
