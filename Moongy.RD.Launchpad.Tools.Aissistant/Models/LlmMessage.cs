
using Moongy.RD.Launchpad.Tools.Aissistant.Enums;

namespace Moongy.RD.Launchpad.Tools.Aissistant.Models;


public class LlmMessage
{
    public LlmRole Role { get; set; }
    public string? Content { get; set; }
}
