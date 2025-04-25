using System;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Registry;

namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.EVM.Providers
{
    /// <summary>
    /// Configurator for builder providers.
    /// </summary>
    public class BuilderProviderConfigurator
    {
        private readonly Action<BuilderProviderRegistry> _configureAction;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BuilderProviderConfigurator"/> class.
        /// </summary>
        /// <param name="configureAction">The action to configure the registry.</param>
        public BuilderProviderConfigurator(Action<BuilderProviderRegistry> configureAction)
        {
            _configureAction = configureAction ?? throw new ArgumentNullException(nameof(configureAction));
        }
        
        /// <summary>
        /// Configures the builder provider registry.
        /// </summary>
        /// <param name="registry">The registry to configure.</param>
        public void Configure(BuilderProviderRegistry registry)
        {
            _configureAction(registry);
        }
    }
}