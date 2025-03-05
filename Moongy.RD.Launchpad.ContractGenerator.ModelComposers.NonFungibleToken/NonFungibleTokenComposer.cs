using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.ContractGenerator.ModelComposers.NonFungibleToken.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.ModelComposers.NonFungibleToken.Models;
using Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Models;

namespace Moongy.RD.Launchpad.ContractGenerator.ModelComposers.NonFungibleToken
{
    public class NonFungibleTokenComposer(INonFungibleTokenParser parser, INonFungibleTokenValidator validator) : INonFungibleTokenComposer
    {
        public SmartContractModel Compose(NonFungibleTokenModel token)
        {
            validator.Validate(token);
            return parser.Parse(token);
        }

        public void Validate(NonFungibleTokenModel token)
        {
            validator.Validate(token);
        }
    }
}
