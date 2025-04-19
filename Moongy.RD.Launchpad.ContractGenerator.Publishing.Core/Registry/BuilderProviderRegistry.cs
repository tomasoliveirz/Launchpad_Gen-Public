using System;
using System.Collections.Generic;
using System.Linq;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces;

namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Registry
{
    /// <summary>
    /// Registry for builder providers that manages the available languages and builders.
    /// </summary>
    public class BuilderProviderRegistry
    {
        private readonly Dictionary<string, IBuilderProvider> _providers = new(StringComparer.OrdinalIgnoreCase);
        
        /// <summary>
        /// Registers a builder provider for a specific language.
        /// </summary>
        /// <param name="provider">The builder provider to register.</param>
        public void RegisterProvider(IBuilderProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));
                
            if (string.IsNullOrEmpty(provider.Language))
                throw new ArgumentException("Provider language cannot be null or empty", nameof(provider));
                
            _providers[provider.Language] = provider;
        }
        
        /// <summary>
        /// Gets a builder for the specified language.
        /// </summary>
        /// <param name="language">The language to get a builder for.</param>
        /// <returns>A contract builder for the specified language.</returns>
        public IContractBuilder GetBuilder(string language)
        {
            if (string.IsNullOrEmpty(language))
                throw new ArgumentException("Language cannot be null or empty", nameof(language));
                
            if (!_providers.TryGetValue(language, out var provider))
                throw new ArgumentException($"No provider registered for language: {language}", nameof(language));
                
            return provider.CreateBuilder();
        }
        
        /// <summary>
        /// Gets all registered languages.
        /// </summary>
        /// <returns>An enumerable of language identifiers.</returns>
        public IEnumerable<string> GetRegisteredLanguages()
        {
            return _providers.Keys.ToList();
        }
        
        /// <summary>
        /// Checks if a provider is registered for the specified language.
        /// </summary>
        /// <param name="language">The language to check.</param>
        /// <returns>True if a provider is registered; otherwise, false.</returns>
        public bool IsLanguageSupported(string language)
        {
            return !string.IsNullOrEmpty(language) && _providers.ContainsKey(language);
        }
    }
}