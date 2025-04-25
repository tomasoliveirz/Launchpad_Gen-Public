namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces
{
    /// <summary>
    /// Represents a provider of contract builders for a specific language.
    /// </summary>
    public interface IBuilderProvider
    {
        /// <summary>
        /// Gets the language supported by this provider.
        /// </summary>
        string Language { get; }
        
        /// <summary>
        /// Creates a contract builder for the language this provider supports.
        /// </summary>
        /// <returns>A contract builder instance.</returns>
        IContractBuilder CreateBuilder();
    }
}