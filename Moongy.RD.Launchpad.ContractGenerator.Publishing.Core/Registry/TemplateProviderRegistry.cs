using System;
using System.Collections.Generic;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces;

namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Registry
{
    /// <summary>
    /// Registry implementation for template providers.
    /// </summary>
    public class TemplateProviderRegistry : ITemplateProviderRegistry
    {
        private readonly Dictionary<string, ITemplateProvider> _providers = 
            new Dictionary<string, ITemplateProvider>(StringComparer.OrdinalIgnoreCase);
        
        /// <inheritdoc/>
        public void RegisterProvider(ITemplateProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));
                
            if (string.IsNullOrEmpty(provider.Language))
                throw new ArgumentException("Provider language cannot be null or empty", nameof(provider));
                
            _providers[provider.Language] = provider;
        }
        
        /// <inheritdoc/>
        public ITemplateProvider GetProvider(string language)
        {
            if (string.IsNullOrEmpty(language))
                throw new ArgumentException("Language cannot be null or empty", nameof(language));
                
            if (!_providers.TryGetValue(language, out var provider))
                throw new InvalidOperationException($"No template provider registered for language: {language}");
                
            return provider;
        }
        
        /// <inheritdoc/>
        public bool HasProvider(string language)
        {
            return !string.IsNullOrEmpty(language) && _providers.ContainsKey(language);
        }
    }
}