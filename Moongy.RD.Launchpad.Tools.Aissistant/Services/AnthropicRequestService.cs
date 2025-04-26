using Moongy.RD.Launchpad.Tools.Aissistant.Enums;
using Moongy.RD.Launchpad.Tools.Aissistant.Exceptions;
using Moongy.RD.Launchpad.Tools.Aissistant.Models;
using Moongy.RD.Launchpad.Tools.Aissistant.Models.Responses.Anthropic;
using Newtonsoft.Json;
namespace Moongy.RD.Launchpad.Tools.Aissistant.Services;
public class AnthropicRequestService(LlmConfiguration config, HttpClient http) : BaseLlmRequestService(config, http)
{
    public async override Task<LlmMessage> ExecuteAsync(string message)
    {
        var (model, temp) = GetModelNameAndTemperature();
        if (model == null) throw new NoAvailableModelForOperationException();
        var messages = SetupUserMessage(message);
        var request = BuildRequest(model, temp, messages);
        var response = await SendRequestAsync(request);
        return ParseResponse(response);
    }

    public override Task<LlmMessage> ExecuteAsync(string message, LlmContext context)
    {
        throw new NotImplementedException();
    }

    protected LlmMessage ParseResponse(string response)
    {
        var responseData = JsonConvert.DeserializeObject<AnthropicResponse>(response);
        var fullText = string.Concat(responseData!.Content.Select(block => block.Text));
        var role = responseData.Role?.ToLowerInvariant() switch
        {
            "assistant" => LlmRole.Assistant,
            "user" => LlmRole.User,
            "system" => LlmRole.System,
            _ => LlmRole.System
        };
        return new LlmMessage() 
        {
            Role    = role,
            Content = fullText
        };
    }


}
