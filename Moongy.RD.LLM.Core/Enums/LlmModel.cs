using Moongy.RD.LLM.Core.Attributes;
using System.Reflection;

namespace Moongy.RD.LLM.Core.Enums;

public enum LlmModel
{
    [LlmSchema(Service = LlmService.OpenAi, Value = "gpt-4")]
    Gpt4,

    [LlmSchema(Service = LlmService.OpenAi, Value = "gpt-3.5-turbo")]
    Gpt35,

    [LlmSchema(Service = LlmService.Claude, Value = "claude-2")]
    Claude2,

    [LlmSchema(Service = LlmService.Claude, Value = "claude-3-opus")]
    Claude3Opus,

    [LlmSchema(Service = LlmService.Mistral, Value = "mistral-7b")]
    Mistral7B,

    [LlmSchema(Service = LlmService.Mistral, Value = "mixtral-8x7b")]
    Mixtral8x7B,

    [LlmSchema(Service = LlmService.Google, Value = "gemini-pro")]
    GeminiPro,

    [LlmSchema(Service = LlmService.Cohere, Value = "command-r-plus")]
    CommandRPlus

}


