using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Generator.Tokenomics.Buyback.Models;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Buyback.Validators
{
    public static class BuybackTokenomicValidator
    {
        public static void Validate(BuybackTokenomicModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            
            if (model.TaxPercentage < 0 || model.TaxPercentage > 100)
                throw new InvalidTokenomicException("Buyback: TaxPercentage must be between 0 and 100.");
        }
    }
}