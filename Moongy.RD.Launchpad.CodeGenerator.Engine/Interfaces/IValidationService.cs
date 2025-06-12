using Moongy.RD.Launchpad.CodeGenerator.Engine.Models;

namespace Moongy.RD.Launchpad.CodeGenerator.Engine.Services
{
    public interface IValidationService
    {
        Task ValidateAsync(ExtractedModels models);
    }
}