using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Engine.Models;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Augmenters.Tax;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Models.Tax;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Augmenters.AccessControl;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Models;

namespace Moongy.RD.Launchpad.CodeGenerator.Engine.Services
{
    public class AugmentationService : IAugmentationService
    {
        private readonly TaxTokenomicAugmenter _taxAugmenter;
        private readonly AccessControlExtensionAugmenter _accessControlAugmenter;

        public AugmentationService(
            TaxTokenomicAugmenter taxAugmenter, 
            AccessControlExtensionAugmenter accessControlAugmenter)
        {
            _taxAugmenter = taxAugmenter;
            _accessControlAugmenter = accessControlAugmenter;
        }

        public async Task AugmentAsync(ContextMetamodel context, ExtractedModels models)
        {
            await Task.Run(() =>
            {
                // apply tokenomics augmenters
                foreach (var tokenomic in models.Tokenomics)
                {
                    ApplyTokenomicAugmenter(context, tokenomic);
                }

                // apply extension augmenters  
                foreach (var extension in models.Extensions)
                {
                    ApplyExtensionAugmenter(context, extension);
                }
            });
        }

        private void ApplyTokenomicAugmenter(ContextMetamodel context, object model)
        {
            switch (model)
            {
                case TaxTokenomicModel tax:
                    _taxAugmenter.Augment(context, tax);
                    break;
                    
                default:
                    throw new NotSupportedException($"Tokenomic type {model.GetType().Name} not supported");
            }
        }

        private void ApplyExtensionAugmenter(ContextMetamodel context, object model)
        {
            switch (model)
            {
                case AccessControlExtensionModel accessControl:
                    _accessControlAugmenter.Augment(context, accessControl);
                    break;
                    
                default:
                    throw new NotSupportedException($"Extension type {model.GetType().Name} not supported");
            }
        }
    }
}