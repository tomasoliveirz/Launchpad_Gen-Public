using System;
using System.Collections.Generic;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Templates;
using Moongy.RD.Launchpad.Core.Enums;


namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.EVM.Providers
{
    /// <summary>
    /// Template provider for Solidity code generation.
    /// Uses Scriban templates to generate Solidity code fragments.
    /// </summary>
    public class SolidityTemplateProvider : ITemplateProvider
    {
        private readonly ITemplateManager _templateManager;
        
        /// <summary>
        /// Gets the language supported by this provider.
        /// </summary>
        public string Language => "Solidity";
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SolidityTemplateProvider"/> class.
        /// </summary>
        /// <param name="templateManager">The template manager to load templates.</param>
        public SolidityTemplateProvider(ITemplateManager templateManager)
        {
            _templateManager = templateManager ?? throw new ArgumentNullException(nameof(templateManager));
        }
        
        /// <inheritdoc/>
        public string GenerateFunction(object function)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function));
                
            if (!(function is SmartContractFunction smartContractFunction))
                throw new ArgumentException("Function must be a SmartContractFunction", nameof(function));
            
            // Load the template content
            string templateContent = _templateManager.GetTemplate("Solidity/Function.scriban");
            
            // Create a template instance
            var template = new ScribanTemplateBase<SmartContractFunction>(templateContent);
            
            // Render and return
            return template.Render(smartContractFunction);
        }
        
        /// <inheritdoc/>
        public string GenerateEvent(object eventModel)
        {
            if (eventModel == null)
                throw new ArgumentNullException(nameof(eventModel));
                
            if (!(eventModel is ContractProperty property))
                throw new ArgumentException("Event must be a ContractProperty", nameof(eventModel));
            
            // Only process event properties
            if (property.PropertyType != PropertyType.Event)
                return string.Empty;
            
            // Load the template content
            string templateContent = _templateManager.GetTemplate("Solidity/Event.scriban");
            
            // Create a template instance
            var template = new ScribanTemplateBase<ContractProperty>(templateContent);
            
            // Render and return
            return template.Render(property);
        }
        
        /// <inheritdoc/>
        public string GenerateImports(object imports)
        {
            if (imports == null)
                throw new ArgumentNullException(nameof(imports));
                
            if (!(imports is List<Import> importsList))
                throw new ArgumentException("Imports must be a List<Import>", nameof(imports));
            
            if (importsList.Count == 0)
                return string.Empty;
            
            // Load the template content
            string templateContent = _templateManager.GetTemplate("Solidity/Import.scriban");
            
            // Create a template instance
            var template = new ScribanTemplateBase<List<Import>>(templateContent);
            
            // Render and return
            return template.Render(importsList);
        }
        
        /// <inheritdoc/>
        public string GenerateStateVariable(object stateVariable)
        {
            if (stateVariable == null)
                throw new ArgumentNullException(nameof(stateVariable));
                
            if (!(stateVariable is ContractProperty property))
                throw new ArgumentException("State variable must be a ContractProperty", nameof(stateVariable));
            
            // Only process non-event properties
            if (property.PropertyType != PropertyType.None)
                return string.Empty;
            
            // Load the template content
            string templateContent = _templateManager.GetTemplate("Solidity/StateVariable.scriban");
            
            // Create a template instance
            var template = new ScribanTemplateBase<ContractProperty>(templateContent);
            
            // Render and return
            return template.Render(property);
        }
    }
}