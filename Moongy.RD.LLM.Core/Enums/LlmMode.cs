using Moongy.RD.LLM.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.LLM.Core.Enums
{
    public enum LlmMode
    {
        [LlmSchema(Service = LlmService.OpenAi, Value = "0.1")]
        [LlmSchema(Service = LlmService.Claude, Value = "0.05")]
        [LlmSchema(Service = LlmService.Mistral, Value = "0.1")]
        [LlmSchema(Service = LlmService.Google, Value = "0.05")]
        UltraPrecise,

        [LlmSchema(Service = LlmService.OpenAi, Value = "0.3")]
        [LlmSchema(Service = LlmService.Claude, Value = "0.2")]
        [LlmSchema(Service = LlmService.Mistral, Value = "0.3")]
        [LlmSchema(Service = LlmService.Google, Value = "0.2")]
        Precise,

        [LlmSchema(Service = LlmService.OpenAi, Value = "0.7")]
        [LlmSchema(Service = LlmService.Claude, Value = "0.5")]
        [LlmSchema(Service = LlmService.Mistral, Value = "0.7")]
        [LlmSchema(Service = LlmService.Google, Value = "0.5")]
        Balanced,

        [LlmSchema(Service = LlmService.OpenAi, Value = "1.1")]
        [LlmSchema(Service = LlmService.Claude, Value = "0.9")]
        [LlmSchema(Service = LlmService.Mistral, Value = "1.1")]
        [LlmSchema(Service = LlmService.Google, Value = "0.9")]
        Creative,

        [LlmSchema(Service = LlmService.OpenAi, Value = "1.5")]
        [LlmSchema(Service = LlmService.Claude, Value = "1.3")]
        [LlmSchema(Service = LlmService.Mistral, Value = "1.5")]
        [LlmSchema(Service = LlmService.Google, Value = "1.3")]
        UltraCreative
    }
}
