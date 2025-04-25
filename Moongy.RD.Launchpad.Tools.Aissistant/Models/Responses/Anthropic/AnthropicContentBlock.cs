using Newtonsoft.Json;

namespace Moongy.RD.Launchpad.Tools.Aissistant.Models.Responses.Anthropic
{
    public class AnthropicContentBlock
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "";

        [JsonProperty("text")]
        public string Text { get; set; } = "";
    }
}
