using Moongy.RD.Launchpad.CodeGenerator.Extensions.Models;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Validators;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Extractors;

public class VotingExtractor : BaseExtensionExtractor<VotingExtensionModel>
{
    public override VotingExtensionModel? Extract(object form)
    {
        var model = base.Extract(form);
        new VotingExtensionValidator().Validate(model);
        if (model != null && model.Type == Enums.VotingTypeEnum.None) model = null;
        if (model != null && model.VotingDelayInHours > 0 && model.VotingPeriodInHours > 0 && model.TimeLock == Enums.VotingTimeLock.None) model = null;
        return model;
    }
}
