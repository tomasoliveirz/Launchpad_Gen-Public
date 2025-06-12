using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Engine.Models;

namespace Moongy.RD.Launchpad.CodeGenerator.Engine.Services
{
    public interface IAugmentationService
    {
        Task AugmentAsync(ContextMetamodel context, ExtractedModels models);
    }
}