using Moongy.RD.Launchpad.CodeGenerator.Engine.Models;

namespace Moongy.RD.Launchpad.CodeGenerator.Engine.Services
{
    public interface IExtractionService
    {
        Task<ExtractedModels> ExtractAsync<TForm>(TForm form) where TForm : class;
    }
}