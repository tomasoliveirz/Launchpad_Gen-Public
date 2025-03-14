
using Moongy.RD.Launchpad.ContractGenerator.Contracts.Core.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.Tokenomics.Core.Interfaces;

namespace Moongy.RD.Launchpad.SmartContractGenerator.Interfaces
{
    public interface ITokenomicCompatibilityRegistry
    {
        bool IsCompatible<TToken, TTokenomic>() where TToken : IToken where TTokenomic : ITokenomic;
        List<Type> GetCompatibleTokenomics<TToken>() where TToken:IToken;
        List<Type> GetCompatibleTokens<TTokenomic>() where TTokenomic:ITokenomic;
        void RegisterCompatibility<TToken, TTokenomic>() where TToken : IToken where TTokenomic : ITokenomic;
    }
}
