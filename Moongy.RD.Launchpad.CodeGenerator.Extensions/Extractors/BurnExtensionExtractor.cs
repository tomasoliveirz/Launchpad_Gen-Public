using Moongy.RD.Launchpad.CodeGenerator.Extensions.ExtensionMethods;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Models;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Extractors;

public class BurnExtensionExtractor : BaseExtensionExtractor<BurnExtensionModel>
{
    public override BurnExtensionModel? Extract(object form)
    {
        var model = base.Extract(form);
        var hasBurn = form.IsExtensionActive(Enums.ExtensionEnum.Burn);

        if (model != null)
            return model;

        if (hasBurn)
        {
            var hasBurningProperty = form.GetType().GetProperty("HasBurning");
            var hasBurningValue = hasBurningProperty?.GetValue(form);

            if (hasBurningValue is bool burning && burning)
                return new BurnExtensionModel();
        }

        return null;
    }
}
