using System;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces;

namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Templates
{
    /// <summary>
    /// Configurator for template providers.
    /// </summary>
    public class TemplateProviderConfigurator : ITemplateProviderConfigurator
    {
        private readonly Action<ITemplateProviderRegistry> _configureAction;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateProviderConfigurator"/> class.
        /// </summary>
        /// <param name="configureAction">The action to configure the registry.</param>
        public TemplateProviderConfigurator(Action<ITemplateProviderRegistry> configureAction)
        {
            _configureAction = configureAction ?? throw new ArgumentNullException(nameof(configureAction));
        }
        
        /// <summary>
        /// Configures the template provider registry.
        /// </summary>
        /// <param name="registry">The registry to configure.</param>
        public void Configure(ITemplateProviderRegistry registry)
        {
            _configureAction(registry);
        }
    }
}