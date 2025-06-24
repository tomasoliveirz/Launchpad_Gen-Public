using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Engine.Models;
using Moongy.RD.Launchpad.CodeGenerator.Engine.Services;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Models;

namespace Engine.Services
{
    public class CompositionService : ICompositionService
    {
        private readonly FungibleTokenComposer _fungibleComposer;

        public CompositionService(FungibleTokenComposer fungibleComposer)
        {
            _fungibleComposer = fungibleComposer;
        }

        public Task<ContextMetamodel> ComposeAsync(ExtractedModels models)
        {
            return Task.Run(() =>
            {
                ModuleFileDefinition mfd = models.Standard switch
                {
                    FungibleTokenModel fungible => _fungibleComposer.Compose(fungible),
                    NonFungibleTokenModel nft   => throw new NotImplementedException(),
                    _ => throw new NotSupportedException(
                        $"Standard type {models.Standard.GetType()} not supported")
                };
                
                return new ContextMetamodel(mfd);
                
            });
        }
    }
}