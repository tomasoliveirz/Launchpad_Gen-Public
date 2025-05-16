using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Models.BuyBack;
using System.ComponentModel.DataAnnotations;

namespace Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Validators.BuyBack
{
    public static class BuyBackTokenomicValidator
    {
        public static void Validate(BuyBackTokenomicModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if ((model.BuyBackFeePercent < 0) || (model.BuyBackFeePercent > 100))
                throw new Exception("Buyback: BuyBackFeePercent must be between 0 and 100.");

            if (model.BuyBackThreshold == 0)
                throw new ValidationException("Buyback: BuyBackThreshold must be greater than 0.");
        }
    }
}
