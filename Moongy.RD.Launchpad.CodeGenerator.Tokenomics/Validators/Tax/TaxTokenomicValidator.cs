using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Models.Tax;
using System.ComponentModel.DataAnnotations;
using Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;

namespace Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Validators.Tax
{
    public class TaxTokenomicValidator : IValidator<TaxTokenomicModel>
    {
        private const double MaxAllowedTax = 5;
        public void Validate(TaxTokenomicModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (model.TaxFee < 0 || model.TaxFee > MaxAllowedTax)
                throw new ValidationException($"Tax: TaxFee must be between 0 and {MaxAllowedTax}.");

            if (model.TaxRecipients == null || !model.TaxRecipients.Any())
                throw new ValidationException("Tax: You must define at least one TaxRecipient.");


            var duplicate = model.TaxRecipients
                                                            .GroupBy(x => x.Address)
                                                            .FirstOrDefault(g => g.Count() > 1);
            if (duplicate != null)
                throw new ValidationException($"Tax: Duplicate TaxRecipient found for address {duplicate.Key}.");
            
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