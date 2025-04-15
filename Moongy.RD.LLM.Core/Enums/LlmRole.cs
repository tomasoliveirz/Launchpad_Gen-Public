using Moongy.RD.LLM.Core.Attributes;

namespace Moongy.RD.LLM.Core.Enums
{
    public enum LlmRole
    {
        [LlmSchema(Service = LlmService.OpenAi, Value = "system")]
        [LlmSchema(Service = LlmService.Claude, Value = "system")]
        [LlmSchema(Service = LlmService.Mistral, Value = "system")]
        [LlmSchema(Service = LlmService.Google, Value = "system")]
        System,

        [LlmSchema(Service = LlmService.OpenAi, Value = "user")]
        [LlmSchema(Service = LlmService.Claude, Value = "user")]
        [LlmSchema(Service = LlmService.Mistral, Value = "user")]
        [LlmSchema(Service = LlmService.Google, Value = "user")]
        User,

        [LlmSchema(Service = LlmService.OpenAi, Value = "assistant")]
        [LlmSchema(Service = LlmService.Claude, Value = "assistant")]
        [LlmSchema(Service = LlmService.Mistral, Value = "assistant")]
        [LlmSchema(Service = LlmService.Google, Value = "model")]
        Assistant
    }
}
