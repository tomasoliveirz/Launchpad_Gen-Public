using Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.ExtensionMethods;

namespace Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Extractors;

public abstract class BaseTokenomicExtractor<TModel> : ITokenomicExtractor<TModel>
    where TModel: class, new() 
{
    public virtual TModel? Extract(object tokenomicFormSection)
    {
        var model = new TModel();
        var modelExists = false;
        
        // copy all primitive properties (everything is automatic)
        foreach (var (prop, attr) in tokenomicFormSection.GetTokenomicProperties())
        {
            if (attr == null) continue;
            var dest = typeof(TModel).GetProperty(attr.Name ?? prop.Name);
            if (dest == null) continue;
            
            var value = prop.GetValue(tokenomicFormSection);
            if (value != null) dest.SetValue(model, value);
            if (!modelExists) modelExists = true;
        }

        return modelExists ? model : null;
    }
}