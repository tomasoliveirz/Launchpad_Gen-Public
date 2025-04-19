using System.Text;

namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Builders
{
    /// <summary>
    /// Base class for components in a builder hierarchy.
    /// Each component can build a specific part of the contract.
    /// </summary>
    public abstract class BuilderComponent
    {
        protected readonly StringBuilder _contentBuilder = new StringBuilder();
        
        /// <summary>
        /// Clears the content of this component.
        /// </summary>
        public virtual void Clear()
        {
            _contentBuilder.Clear();
        }
        
        /// <summary>
        /// Builds the component's content.
        /// </summary>
        /// <returns>The built content as a string.</returns>
        public abstract string Build();
    }
}