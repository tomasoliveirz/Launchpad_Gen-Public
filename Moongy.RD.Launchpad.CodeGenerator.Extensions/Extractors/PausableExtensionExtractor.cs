using Moongy.RD.Launchpad.CodeGenerator.Extensions.ExtensionMethods;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Models;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Extractors;

public class PausableExtensionExtractor : BaseExtensionExtractor<PausableExtensionModel>
{
    public override PausableExtensionModel? Extract(object form)
    {
        var model = base.Extract(form);
        var hasPausable = form.IsExtensionActive(Enums.ExtensionEnum.Pausable);
        return model != null ? model : hasPausable ? new PausableExtensionModel() : null;

    }
}
