namespace Moongy.RD.Launchpad.Tools.Aissistant.Models.Responses.OpenAi;
public class OpenAiResponse
{
    public string? Id { get; set; }
    public string? Object { get; set; }
    public List<OpenAiResponseChoice> Choices { get; set; } = [];
}
