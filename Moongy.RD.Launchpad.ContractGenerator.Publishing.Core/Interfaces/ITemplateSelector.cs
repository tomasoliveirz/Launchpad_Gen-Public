using System;

namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces
{
    /// <summary>
    /// Defines a contract for selecting appropriate templates for a given model.
    /// </summary>
    public interface ITemplateSelector
    {
        /// <summary>
        /// Selects the appropriate template path for the given model type.
        /// </summary>
        /// <param name="modelType">The type of model requiring a template.</param>
        /// <param name="language">The target language (e.g., "Solidity", "Rust").</param>
        /// <returns>The path to the template that should be used.</returns>
        string SelectTemplate(Type modelType, string language);
        
        /// <summary>
        /// Selects the appropriate template path for the given model type and name.
        /// </summary>
        /// <param name="modelType">The type of model requiring a template.</param>
        /// <param name="language">The target language (e.g., "Solidity", "Rust").</param>
        /// <param name="templateName">The specific template name to use, overriding conventions.</param>
        /// <returns>The path to the template that should be used.</returns>
        string SelectTemplate(Type modelType, string language, string templateName);
    }
}