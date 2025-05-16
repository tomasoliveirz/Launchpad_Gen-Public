using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Models.Tax;
using System.ComponentModel.DataAnnotations;

namespace Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Validators.Tax
{
    public static class TaxTokenomicValidator
    {
        public static void Validate(TaxTokenomicModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (model.TaxFee < 0 || model.TaxFee > 100)
                throw new ValidationException("Tax: TaxFee must be between 0 and 100.");

            if (model.TaxRecipients == null || !model.TaxRecipients.Any())
                throw new ValidationException("Tax: You must define at least one TaxRecipient.");

            decimal totalShares = 0;
            foreach (var recipient in model.TaxRecipients)
            {
                if (string.IsNullOrWhiteSpace(recipient.Address))
                    throw new ValidationException("Tax: Each TaxRecipient must have a valid address.");

                if (recipient.Shares <= 0)
                    throw new ValidationException("Tax: Each TaxRecipient must have a share greater than 0.");

                totalShares += recipient.Shares;
            }

            if (totalShares != 100)
                throw new ValidationException("Tax: Sum of all TaxRecipient shares must equal 100.");
        }
    }
}