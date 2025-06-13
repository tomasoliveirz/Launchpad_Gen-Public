using Microsoft.Extensions.DependencyInjection;
using Moongy.RD.Launchpad.CodeGenerator.Engine.Interfaces;
using Moongy.RD.Launchpad.CodeGenerator.Engine.Services;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Generators;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Synthesizer;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Augmenters.Tax;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Augmenters.AccessControl;
using Engine.Services;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Augmenters;

namespace Moongy.RD.Launchpad.CodeGenerator.Engine.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCodeGenerationEngine(this IServiceCollection services)
        {
            // engine
            services.AddScoped<ICodeGenerationEngine, CodeGenerationEngine>();
            
            // core services
            services.AddScoped<IExtractionService, ExtractionService>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<ICompositionService, CompositionService>();
            services.AddScoped<IAugmentationService, AugmentationService>();
            
            // generation services
            services.AddScoped<SoliditySynthesizer>();
            services.AddScoped<SolidityCodeGenerator>();
            
            // composers
            services.AddScoped<FungibleTokenComposer>();
            
            // extension augmenters
            services.AddScoped<AccessControlExtensionAugmenter>();
            services.AddScoped<BurnableExtensionAugmenter>();
            services.AddScoped<MintExtensionAugmenter>();

            // tokenomics augmenters
            services.AddScoped<TaxTokenomicAugmenter>();

            return services;
        }
    }
}