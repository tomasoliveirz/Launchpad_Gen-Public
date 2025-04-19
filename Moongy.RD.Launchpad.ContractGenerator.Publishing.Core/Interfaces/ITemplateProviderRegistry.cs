namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces
{
    /// <summary>
    /// Registry to manage template providers for different languages.
    /// </summary>
    public interface ITemplateProviderRegistry
    {
        /// <summary>
        /// Registers a template provider.
        /// </summary>
        /// <param name="provider">The template provider to register.</param>
        void RegisterProvider(ITemplateProvider provider);
        
        /// <summary>
        /// Gets a template provider for the specified language.
        /// </summary>
        /// <param name="language">The target language.</param>
        /// <returns>The template provider for the language.</returns>
        ITemplateProvider GetProvider(string language);
        
        /// <summary>
        /// Checks if a provider is registered for the specified language.
        /// </summary>
        /// <param name="language">The language to check.</param>
        /// <returns>True if a provider is registered; otherwise, false.</returns>
        bool HasProvider(string language);
    }
}