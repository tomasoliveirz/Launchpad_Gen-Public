using Moongy.RD.Launchpad.Tools.Aissistant.Enums;
using Moongy.RD.Launchpad.Tools.Aissistant.Exceptions;
using Moongy.RD.Launchpad.Tools.Aissistant.Interfaces;
using Moongy.RD.Launchpad.Tools.Aissistant.Models;
using Moongy.RD.Launchpad.Tools.Aissistant.Models.Responses.OpenAi;
using Newtonsoft.Json;

namespace Moongy.RD.Launchpad.Tools.Aissistant.Services;
public class OpenAiRequestService(LlmConfiguration config, HttpClient http) : BaseLlmRequestService(config, http), ILlmRequestService
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

    public async override Task<LlmMessage> ExecuteAsync(string message, LlmContext context)
    {
        throw new NotImplementedException();
    }

    public LlmMessage ParseResponse(string? response)
    {
        if (string.IsNullOrWhiteSpace(response))
            throw new InvalidAissistantResponseException();

        var responseData = JsonConvert.DeserializeObject<OpenAiResponse>(response)
                    ?? throw new InvalidAissistantResponseException();

        var message = responseData.Choices?
            .FirstOrDefault(x => x.Message != null)?
            .Message ?? throw new InvalidAissistantResponseException();

        var role = GetRole(message.Role);

        return new LlmMessage
        {
            Role = role,
            Content = message.Content ?? string.Empty
        };
    }

    private LlmRole GetRole(string? rawRole)
    {

        var role = (rawRole??"").ToLowerInvariant() switch
        {
            "system" => LlmRole.System,
            "user" => LlmRole.User,
            "assistant" => LlmRole.Assistant,
            _ => throw new InvalidLlmRoleException()
        };

        return role;
    }
}
