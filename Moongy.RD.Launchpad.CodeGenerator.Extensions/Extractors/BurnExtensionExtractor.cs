using Moongy.RD.Launchpad.CodeGenerator.Extensions.ExtensionMethods;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Models;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Extractors;

public class BurnExtensionExtractor : BaseExtensionExtractor<BurnExtensionModel>
{
    public override BurnExtensionModel? Extract(object form)
    {
        var model = base.Extract(form);
        var hasBurn = form.IsExtensionActive(Enums.ExtensionEnum.Burn);
        return model != null ? model: hasBurn ? new BurnExtensionModel() : null;
    }
}
