using Microsoft.Extensions.Options;
using Moongy.RD.Launchpad.Tools.Aissistant.Enums;
using Moongy.RD.Launchpad.Tools.Aissistant.Exceptions;
using Moongy.RD.Launchpad.Tools.Aissistant.Extensions;
using Moongy.RD.Launchpad.Tools.Aissistant.Interfaces;
using Moongy.RD.Launchpad.Tools.Aissistant.Models;
using Moongy.RD.Launchpad.Tools.Aissistant.Services;

namespace Moongy.RD.Launchpad.Tools.Aissistant
{
    public class Aissistant(IOptions<AissistantOptions> options)
    {
        private AissistantOptions _options = options.Value;

        public async Task<AissistantResponse> Execute(AissistantRequest request)
        {
            var models = request.Operation.GetBestModelsFor();
            models = models.Where(x => _options.Models.Any(y => y.Model == x));
            if (!models.Any()) throw new NoAvailableModelsForOperationException();

            foreach (var model in models)
            {
                var service = GetService(model) ?? throw new NoAvailableModelsForOperationException();
                var message = GenerateMessage(request.Operation, request.Code, request.Description);
                try
                {
                    var result = await service.ExecuteAsync(message);
                }
                catch
                {
                    continue;
                }
            }
            return new AissistantResponse() { Success = false, Content = "The request could not be handled"};
        }

        private string GenerateMessage(OperationType operation, string? code, string? description)
        {
            throw new NotImplementedException();
        }

        private ILlmRequestService? GetService(LlmModel model)
        {
            var options = _options.Models.FirstOrDefault(x => x.Model == model);
            if (options == null) return null;
            options.DefaultMode ??= _options.DefaultMode ?? LlmMode.Balanced;
            switch (model.GetService())
            {
                case LlmService.OpenAi: return new OpenAiRequestService(options);
                case LlmService.Claude: return new AnthropicRequestService(options);
                case LlmService.Google: return new GoogleLlmRequestService(options);
                default: return null;
            }
        }
    }
}
