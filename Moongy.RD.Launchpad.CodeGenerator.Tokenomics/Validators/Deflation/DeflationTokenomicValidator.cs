using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Models.Deflation;
using System.ComponentModel.DataAnnotations;

namespace Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Validators.Deflation
{
    public static class DeflationTokenomicValidator
    {
        public static void Validate(DeflationTokenomicModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if ((model.DeflationFeePercent < 0) || (model.DeflationFeePercent > 100))
                throw new ValidationException("Deflation: DeflationFeePercent must be between 0 and 100.");
        }
    }
}