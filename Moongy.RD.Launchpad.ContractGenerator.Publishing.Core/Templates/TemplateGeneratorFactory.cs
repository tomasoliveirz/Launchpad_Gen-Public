using System;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces;

namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Templates
{
    /// <summary>
    /// Default implementation of ITemplateGeneratorFactory.
    /// </summary>
    public class TemplateGeneratorFactory : ITemplateGeneratorFactory
    {
        private readonly ITemplateManager _templateManager;
        private readonly ITemplateSelector _templateSelector;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateGeneratorFactory"/> class.
        /// </summary>
        /// <param name="templateManager">The template manager to load templates.</param>
        /// <param name="templateSelector">The template selector to determine which template to use.</param>
        public TemplateGeneratorFactory(ITemplateManager templateManager, ITemplateSelector templateSelector)
        {
            _templateManager = templateManager ?? throw new ArgumentNullException(nameof(templateManager));
            _templateSelector = templateSelector ?? throw new ArgumentNullException(nameof(templateSelector));
        }
        
        /// <inheritdoc/>
        public ITemplate<TModel> CreateGenerator<TModel>(string language, string? customTemplateName = null)
        {
            if (string.IsNullOrEmpty(language))
                throw new ArgumentException("Language cannot be null or empty", nameof(language));
                
            return new GenericTemplateGenerator<TModel>(_templateManager, _templateSelector, language, customTemplateName);
        }
    }
}