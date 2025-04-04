using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Generator.Tokenomics.Reflections.Models;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Reflections.Validators
{
    public static class ReflectionsTokenomicValidator
    {
        public static void Validate(ReflectionsTokenomicModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (model.TaxPercentage < 0 || model.TaxPercentage > 100)
                throw new InvalidTokenomicException("Reflections: TaxPercentage must be between 0 and 100.");

        }
    }
}