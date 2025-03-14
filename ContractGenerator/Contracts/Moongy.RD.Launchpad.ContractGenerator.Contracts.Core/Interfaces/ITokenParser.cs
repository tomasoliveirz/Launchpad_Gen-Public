using Moongy.RD.Launchpad.Core.Models;

namespace Moongy.RD.Launchpad.ContractGenerator.Contracts.Core.Interfaces;
public interface ITokenParser<TToken> where TToken : IToken
{
    public SmartContractModel Parse(TToken tokenModel);
}
