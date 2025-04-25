using Moongy.RD.Launchpad.Tools.Aissistant.Attributes;

namespace Moongy.RD.Launchpad.Tools.Aissistant.Enums
{
    public enum LlmRole
    {
        [LlmSchema(Service = LlmService.OpenAi, Value = "system")]
        [LlmSchema(Service = LlmService.Anthropic, Value = "system")]
        [LlmSchema(Service = LlmService.Google, Value = "system")]
        System,

        [LlmSchema(Service = LlmService.OpenAi, Value = "user")]
        [LlmSchema(Service = LlmService.Anthropic, Value = "user")]
        [LlmSchema(Service = LlmService.Google, Value = "user")]
        User,

        [LlmSchema(Service = LlmService.OpenAi, Value = "assistant")]
        [LlmSchema(Service = LlmService.Anthropic, Value = "assistant")]
        [LlmSchema(Service = LlmService.Google, Value = "model")]
        Assistant
    }
}
