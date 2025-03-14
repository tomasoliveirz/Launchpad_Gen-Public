using Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Models;

namespace Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Interfaces
{
    public interface ITokenValidator<T> where T : TokenModel
    {
        void Validate(T model);
    }
}
