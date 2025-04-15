namespace Moongy.RD.LLM.Core.Models;
public class LlmRequest
{
    public string Model { get; set; }
    public List<LlmMessage> Messages { get; set; } = new();
    public double Temperature { get; set; } = 0.7;
}
