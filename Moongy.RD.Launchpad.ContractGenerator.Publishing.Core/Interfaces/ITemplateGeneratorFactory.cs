namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces
{
    /// <summary>
    /// Factory interface for creating template generators for different model types.
    /// </summary>
    public interface ITemplateGeneratorFactory
    {
        /// <summary>
        /// Creates a template generator for the specified model type and language.
        /// </summary>
        /// <typeparam name="TModel">The type of model to generate templates for.</typeparam>
        /// <param name="language">The target language (e.g., "Solidity", "Rust").</param>
        /// <param name="customTemplateName">Optional custom template name to override conventions.</param>
        /// <returns>A template generator for the specified model type.</returns>
        ITemplate<TModel> CreateGenerator<TModel>(string language, string? customTemplateName = null);
    }
}