using Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Models;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Validators
{
    public class AntiWhaleExtensionValidator : IValidator<AntiWhaleExtensionModel>
    {
        private const decimal MAX_ANTI_WHALE = (decimal)5.0;
        public void Validate(AntiWhaleExtensionModel? model)
        {
            if (model == null || 
                model.CapInPercentage == null || model.CapInPercentage == 0 || 
                (model.CapInPercentage > 0 && model.CapInPercentage <= MAX_ANTI_WHALE )) return;
            throw new ArgumentException($"Anti-whale cap exceeds maximum of 5%");
        }
    }
}
