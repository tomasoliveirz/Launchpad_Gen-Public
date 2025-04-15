using Moongy.RD.LLM.Core.Models;
namespace Moongy.RD.LLM.Core.Interfaces
{
    public interface ILlmService
    {
        Task<string> SendMessageAsync(string message);
        Task<string> SendMessageWithContextAsync(List<LlmMessage> messages);
    }
}
