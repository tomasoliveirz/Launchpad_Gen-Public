using System;
using System.Collections.Generic;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Registry;

namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Builders
{
    /// <summary>
    /// Factory for creating language-specific builders.
    /// </summary>
    public class BuilderFactory
    {
        private readonly BuilderProviderRegistry _providerRegistry;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BuilderFactory"/> class.
        /// </summary>
        /// <param name="providerRegistry">The builder provider registry.</param>
        public BuilderFactory(BuilderProviderRegistry providerRegistry)
        {
            _providerRegistry = providerRegistry ?? throw new ArgumentNullException(nameof(providerRegistry));
        }
        
        /// <summary>
        /// Creates a builder for the specified language.
        /// </summary>
        /// <param name="language">The target language.</param>
        /// <returns>A contract builder for the specified language.</returns>
        public IContractBuilder CreateBuilder(string language)
        {
            if (string.IsNullOrEmpty(language))
                throw new ArgumentException("Language cannot be null or empty", nameof(language));
                
            return _providerRegistry.GetBuilder(language);
        }
        
        /// <summary>
        /// Gets whether the specified language is supported.
        /// </summary>
        /// <param name="language">The language to check.</param>
        /// <returns>True if the language is supported; otherwise, false.</returns>
        public bool IsLanguageSupported(string language)
        {
            return _providerRegistry.IsLanguageSupported(language);
        }
        
        /// <summary>
        /// Gets all registered languages.
        /// </summary>
        /// <returns>An enumerable of language identifiers.</returns>
        public IEnumerable<string> GetSupportedLanguages()
        {
            return _providerRegistry.GetRegisteredLanguages();
        }
    }
}