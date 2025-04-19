using System.Collections.Generic;
using System.Text;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Builders;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces;

namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.EVM.Builders
{
    /// <summary>
    /// Builder for Solidity contracts.
    /// Implements both IContractBuilder for common builder functionality and
    /// ILanguageSpecificBuilder for language-specific features.
    /// </summary>
    public class SolidityContractBuilder : BuilderComponent, IContractBuilder
    {
        private string? _license;
        private string? _pragma;
        private string? _imports;
        private string? _contractDeclaration;
        private readonly List<string> _stateVariables = new List<string>();
        private readonly List<string> _events = new List<string>();
        private readonly List<string> _functions = new List<string>();
        
        /// <summary>
        /// Gets the language this builder is designed for.
        /// </summary>
        public string Language => "Solidity";
        
        /// <summary>
        /// Sets the license for the contract.
        /// </summary>
        public IContractBuilder WithLicense(string license)
        {
            _license = license;
            return this;
        }
        
        /// <summary>
        /// Sets the compiler version pragma for the contract.
        /// </summary>
        public IContractBuilder WithPragma(string version)
        {
            _pragma = $"pragma solidity {version};";
            return this;
        }
        
        /// <summary>
        /// Sets imports for the contract.
        /// </summary>
        public IContractBuilder WithImports(string imports)
        {
            _imports = imports;
            return this;
        }
        
        /// <summary>
        /// Sets the contract declaration.
        /// </summary>
        public IContractBuilder WithContractDeclaration(string name, List<string>? inheritedContracts = null)
        {
            var inheritance = inheritedContracts != null && inheritedContracts.Count > 0 
                ? $" is {string.Join(", ", inheritedContracts)}" 
                : "";
    
            _contractDeclaration = $"contract {name}{inheritance} {{";
            return this;
        }
        
        /// <summary>
        /// Adds a state variable to the contract.
        /// </summary>
        public IContractBuilder WithStateVariable(string variable)
        {
            _stateVariables.Add(variable);
            return this;
        }
        
        /// <summary>
        /// Adds an event to the contract.
        /// </summary>
        public IContractBuilder WithEvent(string eventDefinition)
        {
            _events.Add(eventDefinition);
            return this;
        }
        
        /// <summary>
        /// Adds a function to the contract.
        /// </summary>
        public IContractBuilder WithFunction(string function)
        {
            _functions.Add(function);
            return this;
        }
        
        /// <summary>
        /// Clears the content of this builder.
        /// </summary>
        public override void Clear()
        {
            base.Clear();
            _license = null;
            _pragma = null;
            _imports = null;
            _contractDeclaration = null;
            _stateVariables.Clear();
            _events.Clear();
            _functions.Clear();
        }
        
        /// <summary>
        /// Builds the Solidity contract.
        /// </summary>
        /// <returns>The complete Solidity contract code.</returns>
        public override string Build()
        {
            // Reset the StringBuilder
            _contentBuilder.Clear();
            
            // Add license comment
            if (!string.IsNullOrEmpty(_license))
            {
                _contentBuilder.AppendLine($"// SPDX-License-Identifier: {_license}");
                _contentBuilder.AppendLine();
            }
            
            // Add pragma
            if (!string.IsNullOrEmpty(_pragma))
            {
                _contentBuilder.AppendLine(_pragma);
                _contentBuilder.AppendLine();
            }
            
            // Add imports
            if (!string.IsNullOrEmpty(_imports))
            {
                _contentBuilder.AppendLine(_imports);
                _contentBuilder.AppendLine();
            }
            
            // Add contract declaration
            _contentBuilder.AppendLine(_contractDeclaration);
            _contentBuilder.AppendLine();
            
            // Add events
            if (_events.Count > 0)
            {
                _contentBuilder.AppendLine("    // Events");
                foreach (var eventDef in _events)
                {
                    _contentBuilder.AppendLine($"    {eventDef}");
                }
                _contentBuilder.AppendLine();
            }
            
            // Add state variables
            if (_stateVariables.Count > 0)
            {
                _contentBuilder.AppendLine("    // State variables");
                foreach (var variable in _stateVariables)
                {
                    _contentBuilder.AppendLine($"    {variable}");
                }
                _contentBuilder.AppendLine();
            }
            
            // Add functions
            if (_functions.Count > 0)
            {
                _contentBuilder.AppendLine("    // Functions");
                foreach (var function in _functions)
                {
                    _contentBuilder.AppendLine($"    {function}");
                    _contentBuilder.AppendLine();
                }
            }
            
            // Close contract
            _contentBuilder.AppendLine("}");
            
            return _contentBuilder.ToString();
        }
    }
}