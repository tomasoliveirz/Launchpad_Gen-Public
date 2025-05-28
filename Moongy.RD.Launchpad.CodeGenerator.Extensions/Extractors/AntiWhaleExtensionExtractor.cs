using Moongy.RD.Launchpad.CodeGenerator.Extensions.Models;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Validators;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Extractors;

public class AntiWhaleExtensionExtractor : BaseExtensionExtractor<AntiWhaleExtensionModel>
{
    public override AntiWhaleExtensionModel? Extract(object form)
    {
        var model = base.Extract(form);
        new AntiWhaleExtensionValidator().Validate(model);
        if (model != null && model.CapInPercentage <= 0) model = null;
        return model;
    }
}
