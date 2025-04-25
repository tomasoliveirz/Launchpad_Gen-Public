using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.EVM.Builders;

namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.EVM.Providers
{
    /// <summary>
    /// Provider for Solidity contract builders.
    /// </summary>
    public class SolidityBuilderProvider : IBuilderProvider
    {
        /// <summary>
        /// Gets the language supported by this provider.
        /// </summary>
        public string Language => "Solidity";
        
        /// <summary>
        /// Creates a Solidity contract builder.
        /// </summary>
        /// <returns>A Solidity contract builder.</returns>
        public IContractBuilder CreateBuilder()
        {
            return new SolidityContractBuilder();
        }
    }
}