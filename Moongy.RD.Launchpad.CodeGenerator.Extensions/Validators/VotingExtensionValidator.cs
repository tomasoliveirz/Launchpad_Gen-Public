using Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Models;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Validators;
public class VotingExtensionValidator : IValidator<VotingExtensionModel>
{
    public void Validate(VotingExtensionModel? model)
    {
        if (model == null || model.Type == Enums.VotingTypeEnum.None) return;
        if(model.VotingDelayInHours == 0 || model.VotingPeriodInHours == 0 || model.TimeLock == Enums.VotingTimeLock.None) return;
        if (model.VotingDelayInHours < 0) throw new ArgumentException($"Voting delay cannot be negative");
        if (model.VotingPeriodInHours < 0) throw new ArgumentException($"Voting period cannot be negative");
        if (model.Quorum < 0) throw new ArgumentException($"Quorum cannot be negative");
        if (model.QuorumIsPercentage && model.Quorum > 100) throw new ArgumentException($"Quorum cannot be greater than 100%");
    }
}
