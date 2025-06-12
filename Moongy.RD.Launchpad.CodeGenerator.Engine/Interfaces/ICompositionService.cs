using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Engine.Models;

namespace Moongy.RD.Launchpad.CodeGenerator.Engine.Services
{
    public interface ICompositionService
    {
        Task<ContextMetamodel> ComposeAsync(ExtractedModels models);
    }
}