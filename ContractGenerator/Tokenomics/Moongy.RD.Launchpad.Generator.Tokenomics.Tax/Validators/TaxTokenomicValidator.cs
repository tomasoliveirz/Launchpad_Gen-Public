using System;
using System.Linq;
using Moongy.RD.Launchpad.Generator.Tokenomics.Tax.Models;
using Moongy.RD.Launchpad.Core.Exceptions;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Tax.Validators
{
    public static class TaxTokenomicValidator
    {
        public static void Validate(TaxTokenomicModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (model.TaxRecipients == null || !model.TaxRecipients.Any())
                throw new InvalidTokenomicException("Tax: You must define at least one TaxRecipient.");

            decimal totalShares = 0;
            foreach (var recipient in model.TaxRecipients)
            {
                if (recipient.Address == null || string.IsNullOrWhiteSpace(recipient.Address.ToString()))
                    throw new InvalidTokenomicException("Tax: Each TaxRecipient must have a valid address.");

                if (recipient.Shares <= 0)
                    throw new InvalidTokenomicException("Tax: Each TaxRecipient must have a share greater than 0.");

                totalShares += recipient.Shares;
            }

            if (totalShares > 100)
                throw new InvalidTokenomicException("Tax: Sum of all TaxRecipient shares cannot exceed 100.");
        }
    }
}
