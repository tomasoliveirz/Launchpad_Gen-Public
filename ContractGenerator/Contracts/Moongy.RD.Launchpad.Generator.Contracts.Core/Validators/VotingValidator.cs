using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Enumerables;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;

public static class VotingValidator
{
    public static void Validate(VotingEnum voting)
    {
        if (!Enum.IsDefined(typeof(VotingEnum), voting))
            throw new InvalidEnumException("voting", voting);
    }
}