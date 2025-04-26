using Moongy.RD.Launchpad.Tools.Aissistant.Attributes;

namespace Moongy.RD.Launchpad.Tools.Aissistant.Enums;

public enum LlmMode
{
    [LlmSchema(Service = LlmService.OpenAi, Value = "0.1")]
    [LlmSchema(Service = LlmService.Anthropic, Value = "0.05")]
    UltraPrecise,

    [LlmSchema(Service = LlmService.OpenAi, Value = "0.3")]
    [LlmSchema(Service = LlmService.Anthropic, Value = "0.2")]
    Precise,

    [LlmSchema(Service = LlmService.OpenAi, Value = "0.7")]
    [LlmSchema(Service = LlmService.Anthropic, Value = "0.5")]
    Balanced,

    [LlmSchema(Service = LlmService.OpenAi, Value = "1.1")]
    [LlmSchema(Service = LlmService.Anthropic, Value = "0.9")]
    Creative,

    [LlmSchema(Service = LlmService.OpenAi, Value = "1.5")]
    [LlmSchema(Service = LlmService.Anthropic, Value = "1.3")]
    UltraCreative
}
