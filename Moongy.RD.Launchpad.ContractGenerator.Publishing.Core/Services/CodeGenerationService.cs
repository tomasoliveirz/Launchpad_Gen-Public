using System;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Builders;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces;
using Moongy.RD.Launchpad.Core.Models.Metamodel;
using Moongy.RD.Launchpad.Core.Models.Metamodel.Base;
using Moongy.RD.Launchpad.Core.Enums;

namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Services
{
    /// <summary>
    /// Service for generating code from smart contract models.
    /// </summary>
    public class CodeGenerationService
    {
        private readonly BuilderFactory _builderFactory;
        private readonly ITemplateProviderRegistry _templateProviderRegistry;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CodeGenerationService"/> class.
        /// </summary>
        /// <param name="builderFactory">The builder factory.</param>
        /// <param name="templateProviderRegistry">The template provider registry.</param>
        public CodeGenerationService(
            BuilderFactory builderFactory,
            ITemplateProviderRegistry templateProviderRegistry)
        {
            _builderFactory = builderFactory ?? throw new ArgumentNullException(nameof(builderFactory));
            _templateProviderRegistry = templateProviderRegistry ?? throw new ArgumentNullException(nameof(templateProviderRegistry));
        }
        
        /// <summary>
        /// Generates code for a smart contract model.
        /// </summary>
        /// <param name="model">The smart contract model.</param>
        /// <param name="language">The target language.</param>
        /// <param name="license">The software license.</param>
        /// <param name="version">The compiler version.</param>
        /// <returns>The generated code.</returns>
        public string GenerateCode(SmartContractModel model, string language, SoftwareLicense license, string? version = null)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
                
            if (string.IsNullOrEmpty(language))
                throw new ArgumentException("Language cannot be null or empty", nameof(language));
                
            // Get the appropriate provider for the language
            if (!_templateProviderRegistry.HasProvider(language))
                throw new InvalidOperationException($"No template provider registered for language: {language}");
                
            var templateProvider = _templateProviderRegistry.GetProvider(language);
            
            // Get a builder for the language
            var builder = _builderFactory.CreateBuilder(language);
            
            // Default version if not specified
            string effectiveVersion = version ?? GetDefaultVersionForLanguage(language);
            
            // Configure the builder with basic properties
            builder.WithLicense(license.ToString())
                  .WithPragma(effectiveVersion)
                  .WithContractDeclaration(model.Name);
            
            // Generate imports
            if (model.Imports != null && model.Imports.Count > 0)
            {
                string importsCode = templateProvider.GenerateImports(model.Imports);
                if (!string.IsNullOrEmpty(importsCode))
                {
                    builder.WithImports(importsCode);
                }
            }
            
            // Generate events and state variables
            if (model.Properties != null)
            {
                foreach (var property in model.Properties)
                {
                    if (property.PropertyType == PropertyType.Event)
                    {
                        string eventCode = templateProvider.GenerateEvent(property);
                        if (!string.IsNullOrEmpty(eventCode))
                        {
                            builder.WithEvent(eventCode);
                        }
                    }
                    else if (property.PropertyType == PropertyType.None)
                    {
                        string stateVarCode = templateProvider.GenerateStateVariable(property);
                        if (!string.IsNullOrEmpty(stateVarCode))
                        {
                            builder.WithStateVariable(stateVarCode);
                        }
                    }
                }
            }
            
            // Generate functions
            if (model.SmartContractFunctions != null)
            {
                foreach (var function in model.SmartContractFunctions)
                {
                    string functionCode = templateProvider.GenerateFunction(function);
                    if (!string.IsNullOrEmpty(functionCode))
                    {
                        builder.WithFunction(functionCode);
                    }
                }
            }
            
            // Build the complete code
            return builder.Build();
        }
        
        /// <summary>
        /// Supports generating code from language-specific models.
        /// </summary>
        public string GenerateCodeFromSolidityModel(object solidityModel, string language, SoftwareLicense license, string? version = null)
        {
            // Implementation for SolidityModel would go here
            // This method would be expanded when SolidityModel is implemented
            throw new NotImplementedException("SolidityModel support not yet implemented");
        }
        
        private string GetDefaultVersionForLanguage(string language)
        {
            return language.ToLowerInvariant() switch
            {
                "solidity" => "^0.8.0",
                "rust" => "1.0.0",
                _ => "1.0.0" // Default fallback
            };
        }
    }
}