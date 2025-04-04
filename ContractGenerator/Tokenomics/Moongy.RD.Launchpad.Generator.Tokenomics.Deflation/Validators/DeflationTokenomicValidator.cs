using System;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Validators;
using Moongy.RD.Launchpad.Generator.Tokenomics.Deflation.Models;
using Moongy.RD.Launchpad.Core.Exceptions;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Core.Validators
{
    public static class DeflationTokenomicValidator
    {
        public static void Validate(DeflationTokenomicModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (model.TaxPercentage < 0 || model.TaxPercentage > 100)
                throw new InvalidTokenomicException("Deflation: TaxPercentage must be between 0 and 100.");

        }
    }
}