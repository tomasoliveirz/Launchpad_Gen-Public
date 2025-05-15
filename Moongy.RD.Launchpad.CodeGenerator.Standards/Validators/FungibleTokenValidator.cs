using Moongy.RD.Launchpad.CodeGenerator.Core.Validators;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Models;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Validators
{
    public class FungibleTokenValidator : LaunchpadValidator<FungibleTokenModel>
    {
        public override void Validate(FungibleTokenModel o)
        {
            base.Validate(o);
            if (o.MaxSupply < o.Premint) throw new Exception("Premint should be less or equal to the max supply");
        }
    }
}
