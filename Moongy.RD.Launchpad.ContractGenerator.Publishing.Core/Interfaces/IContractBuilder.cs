using System.Collections.Generic;

namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces
{
    /// <summary>
    /// Interface for a contract builder that follows the builder pattern.
    /// </summary>
    public interface IContractBuilder
    {
        /// <summary>
        /// Builds the complete contract.
        /// </summary>
        /// <returns>The built contract as a string.</returns>
        string Build();
        
        /// <summary>
        /// Sets the license for the contract.
        /// </summary>
        /// <param name="license">The license identifier.</param>
        /// <returns>The builder instance for method chaining.</returns>
        IContractBuilder WithLicense(string license);
        
        /// <summary>
        /// Sets the compiler version pragma for the contract.
        /// </summary>
        /// <param name="version">The compiler version.</param>
        /// <returns>The builder instance for method chaining.</returns>
        IContractBuilder WithPragma(string version);
        
        /// <summary>
        /// Sets imports for the contract.
        /// </summary>
        /// <param name="imports">The import statements.</param>
        /// <returns>The builder instance for method chaining.</returns>
        IContractBuilder WithImports(string imports);
        
        /// <summary>
        /// Sets the contract declaration.
        /// </summary>
        /// <param name="name">The contract name.</param>
        /// <param name="inheritedContracts">Optional list of contracts to inherit from.</param>
        /// <returns>The builder instance for method chaining.</returns>
        IContractBuilder WithContractDeclaration(string name, List<string>? inheritedContracts = null);
        
        /// <summary>
        /// Adds a state variable to the contract.
        /// </summary>
        /// <param name="variable">The state variable declaration.</param>
        /// <returns>The builder instance for method chaining.</returns>
        IContractBuilder WithStateVariable(string variable);
        
        /// <summary>
        /// Adds an event to the contract.
        /// </summary>
        /// <param name="eventDefinition">The event definition.</param>
        /// <returns>The builder instance for method chaining.</returns>
        IContractBuilder WithEvent(string eventDefinition);
        
        /// <summary>
        /// Adds a function to the contract.
        /// </summary>
        /// <param name="function">The function definition.</param>
        /// <returns>The builder instance for method chaining.</returns>
        IContractBuilder WithFunction(string function);
    }
}