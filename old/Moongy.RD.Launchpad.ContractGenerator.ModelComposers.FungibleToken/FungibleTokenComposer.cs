using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.ContractGenerator.ModelComposers.FungibleToken.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.ModelComposers.FungibleToken.Models;
using Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Models;

namespace Moongy.RD.Launchpad.ContractGenerator.ModelComposers.FungibleToken
{
    public class FungibleTokenComposer(IFungibleTokenParser parser, IFungibleTokenValidator validator) : IFungibleTokenComposer
    {
        public SmartContractModel Compose(FungibleTokenModel token)
        {
            validator.Validate(token);
            return parser.Parse(token);
        }

        public void Validate(FungibleTokenModel token)
        {
            validator.Validate(token);
        }
    }
}
