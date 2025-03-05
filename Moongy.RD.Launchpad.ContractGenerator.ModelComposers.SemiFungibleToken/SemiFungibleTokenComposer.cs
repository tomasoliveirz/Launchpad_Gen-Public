
using Moongy.RD.Launchpad.ContractGenerator.ModelComposers.SemiFungibleToken.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.ModelComposers.SemiFungibleToken.Models;
using Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Models;

namespace Moongy.RD.Launchpad.ContractGenerator.ModelComposers.SemiFungibleToken
{
    public class SemiFungibleTokenComposer(ISemiFungibleTokenParser parser, ISemiFungibleTokenValidator validator) : ISemiFungibleTokenComposer
    {
        public SmartContractModel Compose(SemiFungibleTokenModel token)
        {
            validator.Validate(token);
            return parser.Parse(token);
        }

        public void Validate(SemiFungibleTokenModel token)
        {
            validator.Validate(token);
        }
    }
}
