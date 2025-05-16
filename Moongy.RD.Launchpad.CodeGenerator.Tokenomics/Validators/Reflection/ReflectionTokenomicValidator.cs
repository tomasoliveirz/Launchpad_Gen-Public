using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Models.Reflection;
using System.ComponentModel.DataAnnotations;

namespace Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Validators.Reflection
{
    public static class ReflectionTokenomicValidator
    {
        public static void Validate(ReflectionTokenomicModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if ((model.ReflectionFeePercent < 0) || (model.ReflectionFeePercent > 100))
                throw new ValidationException("Reflections: ReflectionFeePercent must be between 0 and 100.");
        }
    }
}