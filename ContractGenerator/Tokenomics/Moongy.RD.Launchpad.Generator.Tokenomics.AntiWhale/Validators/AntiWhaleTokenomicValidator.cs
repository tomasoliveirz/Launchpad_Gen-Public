using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Generator.Tokenomics.AntiWhale.Models;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.AntiWhale.Validators
{
    public static class AntiWhaleTokenomicValidator
    {
        public static void Validate(AntiWhaleTokenomicModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (model.MaxWalletPercentage <= 0 || model.MaxWalletPercentage >= 100)
                throw new InvalidTokenomicException("Anti-Whale: MaxWalletPercentage must be between 0 and 100.");
            
            // não é necessario isto ser obrigatório (?)
            // if (model.NotAplicableAddresses == null)
            //     throw new InvalidTokenomicException("Anti-Whale: NotAplicableAddresses cannot be null.");
        }
    }
}
