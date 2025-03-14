using Moongy.RD.Launchpad.ContractGenerator.ModelComposers.AdvancedFungibleToken.Models;
using Moongy.RD.Launchpad.ContractGenerator.ModelComposers.FungibleToken.Interfaces;
using Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Interfaces;
using Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Models;

namespace Moongy.RD.Launchpad.ContractGenerator.ModelComposers.AdvancedFungibleToken.Interfaces
{
    public interface IAdvancedFungibleTokenComposer : ITokenComposer<AdvancedFungibleTokenModel, IAdvancedFungibleTokenValidator, IAdvancedFungibleTokenParser>
    {
    }
}
