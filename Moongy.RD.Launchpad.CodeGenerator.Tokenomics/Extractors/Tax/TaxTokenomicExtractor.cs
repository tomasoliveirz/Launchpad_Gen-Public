using System.Reflection;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Attributes;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.ExtensionMethods;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Models.Tax;

namespace Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Extractors.Tax;

public class TaxTokenomicExtractor:BaseTokenomicExtractor<TaxTokenomicModel>
{
    public override bool CanHandle(object tokenomicFormSection)
    {
        return tokenomicFormSection.HasTokenomicSource(TokenomicEnum.Tax);
    }

    public override object Extract(object tokenomicFormSection)
    {
        var model = (TaxTokenomicModel) base.Extract(tokenomicFormSection);
        
        var recipientsProp = tokenomicFormSection.GetType().GetProperty("Recipients");
        if (recipientsProp?.GetValue(tokenomicFormSection) is IEnumerable<object> recipientForms)
        {
            model.TaxRecipients = recipientForms.Select(r => new TaxRecipient
            {
                Address = (string)r.GetType().GetProperty("Address")!.GetValue(r)!,
                Shares = (decimal)r.GetType().GetProperty("Shares")!.GetValue(r)!
            }).ToList();
        }
        return model;
    }
    
}