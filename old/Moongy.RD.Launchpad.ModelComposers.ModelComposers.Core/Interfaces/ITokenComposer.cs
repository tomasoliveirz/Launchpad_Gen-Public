using Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Models;

namespace Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Interfaces
{
    public interface ITokenComposer<T, TValidator, TParser> where T : TokenModel
    {
        void Validate(T token);
        SmartContractModel Compose(T token);
    }
}
