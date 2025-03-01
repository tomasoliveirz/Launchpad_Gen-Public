using Moongy.RD.Launchpad.ContractGenerator.ModelComposers.AdvancedFungibleToken.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.ModelComposers.AdvancedFungibleToken.Models;
using Moongy.RD.Launchpad.ContractGenerator.ModelComposers.FungibleToken.Interfaces;
using Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Models;

namespace Moongy.RD.Launchpad.ContractGenerator.ModelComposers.AdvancedFungibleToken
{
    public class AdvancedFungibleTokenComposer(IAdvancedFungibleTokenParser parser, IAdvancedFungibleTokenValidator validator) : IAdvancedFungibleTokenComposer
    {
        public SmartContractModel Compose(AdvancedFungibleTokenModel token)
        {
            validator.Validate(token);
            return parser.Parse(token);
        }

        public void Validate(AdvancedFungibleTokenModel token)
        {
            validator.Validate(token);
        }
    }
}
