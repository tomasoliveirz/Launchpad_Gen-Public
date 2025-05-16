using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Models.LiquidityGeneration;
using System.ComponentModel.DataAnnotations;

namespace Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Validators.LiquidityGeneration
{
    public static class LiquidityGenerationTokenomicValidator
    {
        public static void Validate(LiquidityGenerationTokenomicModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if ((model.LiquidityFeePercent < 0) || (model.LiquidityFeePercent > 100))
                throw new ValidationException("Liquidity Generation: LiquidityFeePercent must be between 0 and 100.");

            if (model.LiquidityThreshold == 0)
                throw new ValidationException("Liquidity Generation: LiquidityThreshold must be greater than 0.");
        }
    }
}