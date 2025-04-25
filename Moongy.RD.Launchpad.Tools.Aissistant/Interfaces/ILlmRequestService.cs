using Moongy.RD.Launchpad.Tools.Aissistant.Enums;
using Moongy.RD.Launchpad.Tools.Aissistant.Models;

namespace Moongy.RD.Launchpad.Tools.Aissistant.Interfaces;
public interface ILlmRequestService
{
    Task<LlmMessage> ExecuteAsync(string message);
    Task<LlmMessage> ExecuteAsync(string message, LlmContext context);
}
