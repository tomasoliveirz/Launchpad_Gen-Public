using Moongy.RD.Launchpad.CodeGenerator.Extensions.ExtensionMethods;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Models;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Extractors;

public class MintExtensionExtractor : BaseExtensionExtractor<MintExtensionModel>
{
    public override MintExtensionModel? Extract(object form)
    {
        var model = base.Extract(form);
        var hasMint = form.IsExtensionActive(Enums.ExtensionEnum.Mint);


        if (model != null)
            return model;

        if (hasMint)
        {
            var hasMintingProperty = form.GetType().GetProperty("HasMinting");
            var hasMintingValue = hasMintingProperty?.GetValue(form);

            if (hasMintingValue is bool mint && mint)
                return new MintExtensionModel();
        }

        return null;
    }
}
