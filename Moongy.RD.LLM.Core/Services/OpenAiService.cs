using Moongy.RD.LLM.Core.Enums;
using Moongy.RD.LLM.Core.Interfaces;
using Moongy.RD.LLM.Core.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Moongy.RD.LLM.Core.ExtensionMethods;
using Newtonsoft.Json;

namespace Moongy.RD.LLM.Core.Services
{
    public class OpenAiService : ILlmService
    {
        private readonly HttpClient _http;
        private readonly ILlmSettings _settings;
        private readonly LlmMode _mode;
        private readonly LlmModel _model;
        private readonly string _url = "https://api.openai.com/v1/chat/completions";

        public OpenAiService(ILlmSettings settings, LlmModel? model, LlmMode? mode)
        {
            _settings = settings;
            _http = new HttpClient();
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _settings.ApiKey);
            _mode = mode ?? LlmMode.Balanced;
            _model = model ?? LlmModel.Gpt4;
        }

        public async Task<LlmContext> SendMessageAsync(string message, LlmModel? alternativeModel, LlmMode? alternativeMode, LlmContext? context)
        {
            context ??= new();
            context.Model = alternativeModel ?? context.Model ?? _model;
            context.Mode = alternativeMode ?? context.Mode ?? _mode;
            context.Messages.Add(new LlmMessage()
            {
                Role = LlmRole.User,
                Content = message
            });

            var messageRequest = new LlmRequest()
            {
                Model = context.Model.Value.Parse(LlmService.OpenAi)!,
                Messages = context.Messages,
                Temperature = context.Mode.Value.Parse(LlmService.OpenAi)!.ToDouble()
            };

            return await Send(context);
        }


        private async Task<LlmContext> Send(LlmContext context)
        {
            var response = await _http.PostAsJsonAsync(_url, new {
                model = context.Model!.Value.Parse(LlmService.OpenAi),
                temperature = context.Mode!.Value.Parse(LlmService.OpenAi)!.ToDouble(),
                messages = context.Messages.Select(m => new { role = m.Role.Parse(LlmService.OpenAi), content = m.Content }),
            });
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<OpenAiResponse>(json);
            var responseMessage = result.Choices.FirstOrDefault().Message;
            context.
        }
    }

    public class OpenAiResponse
    {
        public string? Id { get; set; }
        public string? Object { get; set; }
        public List<OpenAiResponseChoice> Choices { get; set; } = [];

    }
    public class OpenAiResponseChoice
    {
        public int Index { get; set; }
        public OpenAiResponseChoiceMessage? Message { get; set; }
    }

    public class OpenAiResponseChoiceMessage
    {
        public string? Role { get; set; }
        public string? Content { get; set; }
    }
}