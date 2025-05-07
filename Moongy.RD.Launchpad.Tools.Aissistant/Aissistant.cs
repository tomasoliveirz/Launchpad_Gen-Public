using Microsoft.Extensions.Options;
using Moongy.RD.Launchpad.Tools.Aissistant.Constants;
using Moongy.RD.Launchpad.Tools.Aissistant.Enums;
using Moongy.RD.Launchpad.Tools.Aissistant.Exceptions;
using Moongy.RD.Launchpad.Tools.Aissistant.Extensions;
using Moongy.RD.Launchpad.Tools.Aissistant.Interfaces;
using Moongy.RD.Launchpad.Tools.Aissistant.Models;
using Moongy.RD.Launchpad.Tools.Aissistant.Services;

namespace Moongy.RD.Launchpad.Tools.Aissistant
{
    public class Aissistant(IOptions<AissistantOptions> options, HttpClient http) : IAissistant
    {
        private readonly AissistantOptions _options = options.Value;
        private const string _defaultLanguage = "solidity";
        private const string _defaultVersion = "0.8.20";

        public async Task<AissistantResponse> Execute(AissistantRequest request)
        {
            var models = request.Operation.GetBestModelsFor();
            models = models.Where(x => _options.Models.Any(y => y.Model == x));
            if (!models.Any()) throw new NoAvailableModelsForOperationException();

            foreach (var model in models)
            {
                var service = GetService(model) ?? throw new NoAvailableModelsForOperationException();
                var message = GenerateMessage(request);
                try
                {
                    var result = await service.ExecuteAsync(message);
                    return new AissistantResponse() { Success = true, Content = result.Content};
                }
                catch
                {
                    continue;
                }
            }
            return new AissistantResponse() { Success = false, Content = "The request could not be handled"};
        }

        private string GenerateMessage(AissistantRequest request)
        {
            if(request.Language == null)
            {
                request.Language = _defaultLanguage;
                request.Version = _defaultVersion;
            }

            var language = request.Language ?? _defaultLanguage;
            var messageTemplate = Prompts.OperationFor[request.Operation];
            return messageTemplate.Replace("{Version}", request.Version)
                                  .Replace("{Language}", request.Language)
                                  .Replace("{Code}", request.Code)
                                  .Replace("{Description}", request.Description);
        }

        private ILlmRequestService? GetService(LlmModel model)
        {
            var options = _options.Models.FirstOrDefault(x => x.Model == model);
            if (options == null) return null;
            options.DefaultMode ??= _options.DefaultMode ?? LlmMode.Balanced;
            switch (model.GetService())
            {
                case LlmService.OpenAi: return new OpenAiRequestService(options, http);
                case LlmService.Anthropic: return new AnthropicRequestService(options, http);
                default: return null;
            }
        }
    }
}
