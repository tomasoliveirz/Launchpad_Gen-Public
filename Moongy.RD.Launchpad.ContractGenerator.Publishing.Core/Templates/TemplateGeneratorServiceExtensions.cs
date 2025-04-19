using Microsoft.Extensions.DependencyInjection;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Builders;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces;

namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Templates
{
    /// <summary>
    /// Extension methods for registering template generator services.
    /// </summary>
    public static class TemplateGeneratorServiceExtensions
    {
        /// <summary>
        /// Adds template generator services to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="templatesBasePath">Optional base path for templates.</param>
        /// <returns>The service collection for chaining.</returns>
        public static IServiceCollection AddTemplateGeneratorServices(
            this IServiceCollection services,
            string? templatesBasePath = null)
        {
            // Determine the templates base path
            var basePath = templatesBasePath ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates");
            
            // Register core components
            services.AddSingleton<ITemplateManager>(provider => new FileSystemTemplateManager(basePath));
            services.AddSingleton<ITemplateSelector, ConventionBasedTemplateSelector>();
            services.AddSingleton<ITemplateGeneratorFactory, TemplateGeneratorFactory>();
            
            // Register builder components
            services.AddSingleton<BuilderFactory>();
            
            return services;
        }
    }
}