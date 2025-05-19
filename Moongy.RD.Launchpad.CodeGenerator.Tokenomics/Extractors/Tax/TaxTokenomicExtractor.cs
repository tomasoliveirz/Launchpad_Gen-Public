using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.ExtensionMethods;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Models.Tax;

namespace Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Extractors.Tax;

public class TaxTokenomicExtractor
{
    public TaxTokenomicModel Extract(object tokenomicFormSection)
    {
        var taxSectionObject = tokenomicFormSection.GetType().GetProperty("Tax")?.GetValue(tokenomicFormSection);
        if (taxSectionObject == null) throw new InvalidOperationException("Form does not contain a Tax section");
        var model = new TaxTokenomicModel();

        foreach (var (prop, attr) in taxSectionObject.GetTokenomicProperties())
        {
            var targetProp = typeof(TaxTokenomicModel).GetProperty(attr!.Name ?? prop.Name);
            if (targetProp == null) continue;
            
            var value = prop.GetValue(taxSectionObject);
            if (value == null) continue;

            if (targetProp.PropertyType == typeof(List<TaxRecipient>))
            {
                var recipients = ((IEnumerable<object>)value)
                    .Select(r => new TaxRecipient
                    {
                        Address = (string)r.GetType().GetProperty("Address")!.GetValue(r)!,
                        Shares = (decimal)(r.GetType().GetProperty("Shares")!.GetValue(r)!)
                    })
                    .ToList();
                targetProp.SetValue(model, recipients);
                
            }
            else
            {
                targetProp.SetValue(model, value);
            }
        }
        return model;
    }
    
}