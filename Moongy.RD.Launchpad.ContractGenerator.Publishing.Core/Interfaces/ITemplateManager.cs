using System.Threading.Tasks;

namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for a component that manages and retrieves template content.
    /// </summary>
    public interface ITemplateManager
    {
        /// <summary>
        /// Retrieves template content by template path.
        /// </summary>
        /// <param name="templatePath">The path to the template.</param>
        /// <returns>The template content as a string.</returns>
        string GetTemplate(string templatePath);
        
        /// <summary>
        /// Clears the internal template cache.
        /// </summary>
        void ClearCache();
    }
}