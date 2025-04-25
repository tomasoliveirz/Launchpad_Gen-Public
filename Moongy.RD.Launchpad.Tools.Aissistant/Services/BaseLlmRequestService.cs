using Moongy.RD.Launchpad.Tools.Aissistant.Enums;
using Moongy.RD.Launchpad.Tools.Aissistant.Extensions;
using Moongy.RD.Launchpad.Tools.Aissistant.Interfaces;
using Moongy.RD.Launchpad.Tools.Aissistant.Models;
using Newtonsoft.Json;

namespace Moongy.RD.Launchpad.Tools.Aissistant.Services;
public abstract class BaseLlmRequestService(LlmConfiguration config) : ILlmRequestService
{
    protected readonly HttpClient _http;
    protected readonly LlmConfiguration _settings = config;

    public abstract Task<LlmMessage> ExecuteAsync(string message);
    public abstract Task<LlmMessage> ExecuteAsync(string message, LlmContext context);

    protected (string?, double?) GetModelNameAndTemperature()
    {
        var model = _settings.Model.Parse();
        var service = _settings.Model.GetService();
        var temp = (_settings.DefaultMode ?? LlmMode.Balanced).Parse(service).ToDouble();
        return (model, temp);
    }

    protected List<LlmMessage> SetupUserMessage(string message)
    {
        return [new() { Role = LlmRole.User, Content = message }];
    }

    protected virtual object BuildRequest(string model, double? temp, IEnumerable<LlmMessage> messages)
    {
        var dict = new Dictionary<string, object>
        {
            ["model"] = model,
            ["messages"] = messages
            .Select(x => new {
                role = x.Role.Parse(LlmService.Anthropic),
                content = x.Content
            })
            .ToArray()
        };
        if (temp.HasValue)
            dict["temperature"] = temp.Value;
        return dict;
    }




    protected virtual async Task<string> SendRequestAsync(object request)
    {
        var opts = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var payload = JsonSerializer.Serialize(request, opts);

        using var httpReq = new HttpRequestMessage(HttpMethod.Post, _settings.Endpoint)
        {
            Content = new StringContent(payload, Encoding.UTF8, "application/json")
        };
        httpReq.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", _settings.Token);

        var resp = await _http.SendAsync(httpReq);
        resp.EnsureSuccessStatusCode();

        return await resp.Content.ReadAsStringAsync();
    }


}
