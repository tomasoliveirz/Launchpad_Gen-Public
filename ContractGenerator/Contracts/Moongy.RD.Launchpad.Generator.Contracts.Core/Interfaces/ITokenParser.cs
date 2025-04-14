using Moongy.RD.Launchpad.Core.Models.Metamodel;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces;
public interface ITokenParser<TToken> where TToken : IToken
{
    public SmartContractModel Parse(TToken tokenModel);
}
