using Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.ExtensionMethods;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Extractors;

public abstract class BaseExtensionExtractor<TModel>(ExtensionEnum extension) : IExtensionExtractor
    where TModel: class, new()
{
    public abstract bool CanHandle(object extensionFormSection);
    
    public virtual object Extract(object extensionFormSection)
    {
        var model = new TModel();
        foreach (var (prop, attr) in extensionFormSection.GetExtensionProperties())
        {
            var dest = typeof(TModel).GetProperty(attr!.Name ?? prop.Name);
            if (dest == null) continue;
            
            var value = prop.GetValue(extensionFormSection);
            if (value != null) dest.SetValue(model, value);
        }
        return model;
    }
    
}

