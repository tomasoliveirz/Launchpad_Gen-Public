using Moongy.RD.Launchpad.CodeGenerator.Engine.Models;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Models.Tax;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Models;

namespace Moongy.RD.Launchpad.CodeGenerator.Engine.Services
{
    public class ValidationService : IValidationService
    {
        public Task ValidateAsync(ExtractedModels models)
        {
            return Task.Run(() =>
            {
                ValidateTokenomicsRequirements(models);
                ValidateExtensionRequirements(models);
            });
        }

        private void ValidateTokenomicsRequirements(ExtractedModels models)
        {
            var hasTax = models.Tokenomics.Any(t => t is TaxTokenomicModel);
            var hasAccessControl = models.Extensions.Any(e => e is AccessControlExtensionModel);

            if (hasTax && !hasAccessControl)
            {
                throw new InvalidOperationException(
                    "Tax functionality requires Access Control to be enabled. " +
                    "Tax features need owner permissions to manage tax settings."
                );
            }
        }

        private void ValidateExtensionRequirements(ExtractedModels models)
        {
            var hasMint = models.Extensions.Any(e => e is MintExtensionModel);
            var hasBurn = models.Extensions.Any(e => e is BurnExtensionModel);
            var hasAccessControl = models.Extensions.Any(e => e is AccessControlExtensionModel);

            if (hasMint && !hasAccessControl)
            {
                throw new InvalidOperationException(
                    "Mint functionality requires Access Control to be enabled. " +
                    "Mint functions need proper permission controls to prevent unauthorized token creation."
                );
            }

            if (hasBurn && !hasAccessControl)
            {
                throw new InvalidOperationException(
                    "Burn functionality requires Access Control to be enabled. " +
                    "While users can burn their own tokens, burnFrom needs allowance controls."
                );
            }
        }
    }
}