using System;
using System.IO;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces;

namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Templates
{
    /// <summary>
    /// Selects templates based on naming conventions.
    /// The convention is: {Language}/{ModelName}.scriban
    /// </summary>
    public class ConventionBasedTemplateSelector : ITemplateSelector
    {
        private readonly string _templateExtension;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ConventionBasedTemplateSelector"/> class.
        /// </summary>
        /// <param name="templateExtension">The file extension for templates. Defaults to ".scriban".</param>
        public ConventionBasedTemplateSelector(string templateExtension = ".scriban")
        {
            _templateExtension = templateExtension;
        }
        
        /// <inheritdoc/>
        public string SelectTemplate(Type modelType, string language)
        {
            if (modelType == null)
                throw new ArgumentNullException(nameof(modelType));
                
            if (string.IsNullOrEmpty(language))
                throw new ArgumentNullException(nameof(language));
                
            // Extract the model name from the type
            string modelName = modelType.Name;
            
            // Remove "Model" suffix if present
            if (modelName.EndsWith("Model"))
            {
                modelName = modelName.Substring(0, modelName.Length - 5);
            }
            
            // Construct the template path
            return Path.Combine(language, $"{modelName}{_templateExtension}");
        }
        
        /// <inheritdoc/>
        public string SelectTemplate(Type modelType, string language, string templateName)
        {
            if (string.IsNullOrEmpty(templateName))
                return SelectTemplate(modelType, language);
                
            // If a specific template name is provided, use it
            return Path.Combine(language, $"{templateName}{_templateExtension}");
        }
    }
}