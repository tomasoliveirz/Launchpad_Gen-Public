using System;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Validators;
using Moongy.RD.Launchpad.Generator.Tokenomics.LiquidityGeneration.Models;
using Moongy.RD.Launchpad.Core.Exceptions;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Core.Validators
{
    public static class LiquidityGenerationTokenomicValidator
    {
        public static void Validate(LiquidityGenerationTokenomicModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            // TaxPercentage should be between 0 and 100
            if (model.TaxPercentage < 0 || model.TaxPercentage > 100)
                throw new InvalidTokenomicException("Liquidity Generation: TaxPercentage must be between 0 and 100.");

            // Additional rules for Liquidity Generation can be added here.
        }
    }
}