using Moongy.RD.LLM.Core.Enums;

namespace Moongy.RD.LLM.Core.Models;

public class LlmMessage
{
    public LlmRole Role { get; set; }
    public string? Content { get; set; }
}
