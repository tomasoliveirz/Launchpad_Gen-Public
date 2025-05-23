using System.Reflection;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Attributes;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.ExtensionMethods;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Models.Tax;

namespace Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Extractors.Tax;

public class TaxTokenomicExtractor : BaseTokenomicExtractor<TaxTokenomicModel>
{

    public override TaxTokenomicModel Extract(object tokenomicFormSection)
    {
        var model = base.Extract(tokenomicFormSection);

        var recipientsProp = tokenomicFormSection.GetType().GetProperty(nameof(TaxTokenomicModel.TaxRecipients));
        if (recipientsProp?.GetValue(tokenomicFormSection) is IEnumerable<object> recipientForms)
        {
            model.TaxRecipients = recipientForms.Select(r => new TaxRecipient
            {
                Address = (string)r.GetType().GetProperty(nameof(TaxRecipient.Address))!.GetValue(r)!,
                Shares = (decimal)r.GetType().GetProperty(nameof(TaxRecipient.Shares))!.GetValue(r)!,
            }).ToList();
        }
        return model;
    }
    
}