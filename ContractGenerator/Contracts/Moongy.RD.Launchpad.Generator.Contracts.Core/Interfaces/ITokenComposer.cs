using Moongy.RD.Launchpad.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces
{
    public interface ITokenComposer<TToken> where TToken : IToken
    {
        public SmartContractModel Compose(TToken tokenModel);
        public void Validate(TToken tokenModel);
    }
}
