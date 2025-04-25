using Moongy.RD.Launchpad.Tools.Aissistant.Attributes;

namespace Moongy.RD.Launchpad.Tools.Aissistant.Enums
{
    public enum LlmModel
    {
        None,
        [LlmSchema(Service = LlmService.OpenAi, Value = "gpt-4", BestFor = [OperationType.Generate, OperationType.Analyze, OperationType.Optimize])]
        Gpt4,

        [LlmSchema(Service = LlmService.OpenAi, Value = "gpt-3.5-turbo", BestFor = [OperationType.Document, OperationType.Format])]
        Gpt35,

        [LlmSchema(Service = LlmService.Anthropic, Value = "claude-2", BestFor = [OperationType.Analyze, OperationType.Document])]
        Claude2,

        [LlmSchema(Service = LlmService.Anthropic, Value = "claude-3-opus", BestFor = [OperationType.Optimize, OperationType.Generate])]
        Claude3Opus,

    }
}
