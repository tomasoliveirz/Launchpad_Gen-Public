
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces;
public interface ITokenParser<TToken> where TToken : IToken
{
    // TODO Needs to be replaced by a generic type
    public SolidityContractModel Parse(TToken tokenModel);
}
