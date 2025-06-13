using Moongy.RD.Launchpad.CodeGenerator.Extensions.ExtensionMethods;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Models;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Extractors;

public class MintExtensionExtractor : BaseExtensionExtractor<MintExtensionModel>
{
    public override MintExtensionModel? Extract(object form)
    {
        var model = base.Extract(form);
        var hasMint = form.IsExtensionActive(Enums.ExtensionEnum.Mint);
        return model != null ? model : hasMint ? new MintExtensionModel() : null;
    }
}
