using Moongy.RD.Launchpad.Tools.Aissistant.Enums;
using Moongy.RD.Launchpad.Tools.Aissistant.Exceptions;
using Moongy.RD.Launchpad.Tools.Aissistant.Interfaces;
using Moongy.RD.Launchpad.Tools.Aissistant.Models;

namespace Moongy.RD.Launchpad.Tools.Aissistant.Services;
public class OpenAiRequestService(LlmConfiguration config) : BaseLlmRequestService(config), ILlmRequestService
{
    public override Task<LlmMessage> ExecuteAsync(string message)
    {
        var (model, temp) = GetModelNameAndTemperature();
        if (model == null) throw new NoAvailableModelForOperationException();
        var messages = SetupUserMessage(message);
        var request = BuildRequest(model, temp, messages);
        var response = await SendRequestAsync(request);
        return ParseResponse(response);
    }

    public Task<LlmMessage> ExecuteAsync(string message, LlmContext context)
    {
        throw new NotImplementedException();
    }
}
