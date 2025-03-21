using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.Generator.Contracts.AdvanceFungibleToken.Models;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;

namespace Moongy.RD.Launchpad.Generator.Contracts.AdvanceFungibleToken.Validators
{
    public class AdvancedFungibleTokenValidator : BaseTokenValidator<AdvancedFungibleTokenModel>
    {
        public override void Validate(AdvancedFungibleTokenModel token)
        {
            base.Validate(token);

        }
    }
}
