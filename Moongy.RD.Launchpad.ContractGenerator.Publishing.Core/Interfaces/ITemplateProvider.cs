namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces
{
    /// <summary>
    /// Provider interface for template generation specific to a language.
    /// Resolves language-specific template operations without creating dependency loops.
    /// </summary>
    public interface ITemplateProvider
    {
        /// <summary>
        /// Gets the target language for this provider.
        /// </summary>
        string Language { get; }
        
        /// <summary>
        /// Generates code for a function model.
        /// </summary>
        /// <param name="function">The function model.</param>
        /// <returns>The generated code as string.</returns>
        string GenerateFunction(object function);
        
        /// <summary>
        /// Generates code for an event model.
        /// </summary>
        /// <param name="eventModel">The event model.</param>
        /// <returns>The generated code as string.</returns>
        string GenerateEvent(object eventModel);
        
        /// <summary>
        /// Generates code for imports.
        /// </summary>
        /// <param name="imports">The imports collection.</param>
        /// <returns>The generated code as string.</returns>
        string GenerateImports(object imports);
        
        /// <summary>
        /// Generates code for a state variable.
        /// </summary>
        /// <param name="stateVariable">The state variable model.</param>
        /// <returns>The generated code as string.</returns>
        string GenerateStateVariable(object stateVariable);
    }
}