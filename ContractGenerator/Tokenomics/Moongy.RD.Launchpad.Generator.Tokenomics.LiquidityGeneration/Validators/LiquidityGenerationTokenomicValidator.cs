using System;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Validators;
using Moongy.RD.Launchpad.Generator.Tokenomics.LiquidityGeneration.Models;
using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Enums;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Core.Validators
{
    public static class LiquidityGenerationTokenomicValidator
    {
        public static void Validate(LiquidityGenerationTokenomicModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (model.TaxPercentage < 0 || model.TaxPercentage > 100)
                throw new InvalidTokenomicException("Liquidity Generation: TaxPercentage must be between 0 and 100.");
            
            if (model.TaxPercentage > 3 && model.TriggerMode != TokenomicTriggerMode.Manual)
                throw new InvalidTokenomicException("Liquidity Generation: with TaxPercentage above 3% in Automatic Mode, is highly recommended to use Manual Mode to reduce computational costs.");
            
            if (model.TaxCollector == null || string.IsNullOrWhiteSpace(model.TaxCollector.ToString()))
                throw new InvalidTokenomicException("Liquidity Generation: TaxCollector must be a valid address.");
        }
    }
}