namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces
{
    /// <summary>
    /// Interface for components that configure template providers.
    /// </summary>
    public interface ITemplateProviderConfigurator
    {
        /// <summary>
        /// Configures the template provider registry.
        /// </summary>
        /// <param name="registry">The registry to configure.</param>
        void Configure(ITemplateProviderRegistry registry);
    }
}