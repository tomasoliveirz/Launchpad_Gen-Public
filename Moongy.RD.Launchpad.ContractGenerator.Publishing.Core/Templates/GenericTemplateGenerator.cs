using System;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces;

namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Templates
{
    /// <summary>
    /// A generic template generator that selects the appropriate template and renders it for a given model.
    /// This is the central component that orchestrates the template selection and rendering process.
    /// </summary>
    /// <typeparam name="TModel">The type of model to generate templates for.</typeparam>
    public class GenericTemplateGenerator<TModel> : ITemplate<TModel>
    {
        private readonly ITemplateManager _templateManager;
        private readonly ITemplateSelector _templateSelector;
        private readonly string _language;
        private readonly string? _customTemplateName;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTemplateGenerator{TModel}"/> class.
        /// </summary>
        /// <param name="templateManager">The template manager to load templates.</param>
        /// <param name="templateSelector">The template selector to determine which template to use.</param>
        /// <param name="language">The target language (e.g., "Solidity", "Rust").</param>
        /// <param name="customTemplateName">Optional custom template name to override conventions.</param>
        public GenericTemplateGenerator(
            ITemplateManager templateManager,
            ITemplateSelector templateSelector,
            string language,
            string? customTemplateName = null)
        {
            _templateManager = templateManager ?? throw new ArgumentNullException(nameof(templateManager));
            _templateSelector = templateSelector ?? throw new ArgumentNullException(nameof(templateSelector));
            _language = language ?? throw new ArgumentNullException(nameof(language));
            _customTemplateName = customTemplateName;
        }
        
        /// <inheritdoc/>
        public string Render(TModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
                
            // Determine the template to use
            string templatePath = string.IsNullOrEmpty(_customTemplateName) 
                ? _templateSelector.SelectTemplate(typeof(TModel), _language)
                : _templateSelector.SelectTemplate(typeof(TModel), _language, _customTemplateName);
                
            // Get the template content
            string templateContent = _templateManager.GetTemplate(templatePath);
            
            // Create a Scriban template instance
            var template = new ScribanTemplateBase<TModel>(templateContent);
            
            // Render and return the result
            return template.Render(model);
        }
    }
}