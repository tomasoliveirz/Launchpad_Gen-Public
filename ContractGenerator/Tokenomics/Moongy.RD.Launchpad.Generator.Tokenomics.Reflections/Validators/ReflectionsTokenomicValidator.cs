using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Enums;
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
            
            // verificação para o modo trigger manual 
            if (model.TriggerMode != TokenomicTriggerMode.Manual && model.TaxPercentage > 5)
                throw new InvalidTokenomicException("Reflections: with TaxPercentage above 10% in Automatic Mode, is highly recommended to use Manual Mode. Consider using Manual Mode instead or reducing the tax percentage.");
        }
    }
}