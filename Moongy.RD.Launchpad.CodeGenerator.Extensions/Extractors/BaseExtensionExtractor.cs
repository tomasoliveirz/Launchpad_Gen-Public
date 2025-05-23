using Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.ExtensionMethods;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Extractors;

public abstract class BaseExtensionExtractor<TModel> : IExtensionExtractor<TModel>
    where TModel : class, new()
{
    public virtual TModel Extract(object extensionFormSection)
    {
        var model = new TModel();

        foreach (var (prop, attr) in extensionFormSection.GetExtensionProperties())
        {
            var dest = typeof(TModel).GetProperty(attr.Name ?? prop.Name);
            if (dest == null) continue;

            var value = prop.GetValue(extensionFormSection);
            if (value != null) dest.SetValue(model, value);
        }

        return model;
    }
}

