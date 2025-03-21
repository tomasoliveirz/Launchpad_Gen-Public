using Moongy.RD.Launchpad.Core.Interfaces;

namespace Moongy.RD.Launchpad.ContractGenerator.Contracts.Core.Interfaces;
public interface ITokenValidator<TToken> : IValidator<TToken> where TToken : IToken
{
}
