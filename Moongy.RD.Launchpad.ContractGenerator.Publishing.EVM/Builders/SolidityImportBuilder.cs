using System.Collections.Generic;
using System.Text;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Builders;

namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.EVM.Builders
{
    /// <summary>
    /// A builder component specifically for Solidity import statements.
    /// This builder formats and organizes import statements according to Solidity conventions.
    /// </summary>
    public class SolidityImportBuilder : BuilderComponent
    {
        private readonly List<string> _imports = new List<string>();
        
        /// <summary>
        /// Adds an import statement to the builder.
        /// </summary>
        /// <param name="path">The path to import from.</param>
        /// <returns>The builder for method chaining.</returns>
        public SolidityImportBuilder AddImport(string path)
        {
            if (!string.IsNullOrEmpty(path) && !_imports.Contains(path))
            {
                _imports.Add(path);
            }
            return this;
        }
        
        /// <summary>
        /// Adds multiple import statements to the builder.
        /// </summary>
        /// <param name="paths">The paths to import from.</param>
        /// <returns>The builder for method chaining.</returns>
        public SolidityImportBuilder AddImports(IEnumerable<string> paths)
        {
            if (paths == null) return this;
            
            foreach (var path in paths)
            {
                AddImport(path);
            }
            return this;
        }
        
        /// <summary>
        /// Clears all import statements from the builder.
        /// </summary>
        public override void Clear()
        {
            base.Clear();
            _imports.Clear();
        }
        
        /// <summary>
        /// Builds the import statements according to Solidity conventions.
        /// </summary>
        /// <returns>The formatted import statements as a string.</returns>
        public override string Build()
        {
            _contentBuilder.Clear();
            
            if (_imports.Count == 0)
            {
                return string.Empty;
            }
            
            // Sort imports for consistent output
            _imports.Sort();
            
            foreach (var importPath in _imports)
            {
                _contentBuilder.AppendLine($"import \"{importPath}\";");
            }
            
            return _contentBuilder.ToString();
        }
    }
}