using Moongy.RD.Launchpad.CodeGenerator.Engine.Models;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Models.Tax;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Models;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Validators;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Models;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Validators;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Validators.Tax;

namespace Moongy.RD.Launchpad.CodeGenerator.Engine.Services
{
    public class ValidationService : IValidationService
    {
        private readonly TaxTokenomicValidator _taxValidator;
        private readonly AccessControlExtensionValidator _accessControlValidator;
        private readonly FungibleTokenValidator _fungibleTokenValidator;

        public ValidationService()
        {
            _taxValidator = new TaxTokenomicValidator();
            _accessControlValidator = new AccessControlExtensionValidator();
        }

        public Task ValidateAsync(ExtractedModels models)
        {
            return Task.Run(() =>
            {
                ValidateTokenomics(models);
                ValidateExtensions(models);
                ValidateStandard(models);
                
                ValidateTokenomicsRequirements(models);
                ValidateExtensionRequirements(models);
            });
        }
        private void ValidateStandard(ExtractedModels models)
        {
            if (models.Standard == null)
            {
                throw new InvalidOperationException("Standard model is not defined.");
            }

            switch (models.Standard)
            {
                case FungibleTokenModel fungible:
                    _fungibleTokenValidator.Validate(fungible);
                    break;
                    
                case NonFungibleTokenModel nft:
                    break;
                    
                default:
                    throw new NotSupportedException($"Standard type {models.Standard.GetType().Name} is not supported.");
            }
        }
        private void ValidateTokenomics(ExtractedModels models)
        {
            foreach (var tokenomic in models.Tokenomics)
            {
                switch (tokenomic)
                {
                    case TaxTokenomicModel tax:
                        _taxValidator.Validate(tax);
                        break;
                    
                    default:
                        break;
                }
            }
        }

        private void ValidateExtensions(ExtractedModels models)
        {
            foreach (var extension in models.Extensions)
            {
                switch (extension)
                {
                    case AccessControlExtensionModel accessControl:
                        _accessControlValidator.Validate(accessControl);
                        break;
                        
                    case AntiWhaleExtensionModel antiWhale:
                    case VotingExtensionModel voting:
                    case BurnExtensionModel:
                    case MintExtensionModel:
                        break;
                        
                    default:
                        break;
                }
            }
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