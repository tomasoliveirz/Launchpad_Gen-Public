using Moongy.RD.Launchpad.ContractGenerator.ModelComposers.NonFungibleToken.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.ModelComposers.NonFungibleToken.Models;
using Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Models;
namespace Moongy.RD.Launchpad.ContractGenerator.ModelComposers.NonFungibleToken.Interfaces
{
    public interface INonFungibleTokenComposer : ITokenComposer<NonFungibleTokenModel, INonFungibleTokenValidator, INonFungibleTokenParser>
    {

    }
}
