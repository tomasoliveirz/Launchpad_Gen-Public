using Newtonsoft.Json;

namespace Moongy.RD.Launchpad.Tools.Aissistant.Models.Responses.Anthropic
{
    public class AnthropicResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; } = "";

        [JsonProperty("type")]
        public string Type { get; set; } = "";

        [JsonProperty("role")]
        public string Role { get; set; } = "";

        [JsonProperty("content")]
        public List<AnthropicContentBlock> Content { get; set; } = new();

        [JsonProperty("stop_reason")]
        public string StopReason { get; set; } = "";

        [JsonProperty("usage")]
        public AnthropicUsage Usage { get; set; } = new();
    }
}
