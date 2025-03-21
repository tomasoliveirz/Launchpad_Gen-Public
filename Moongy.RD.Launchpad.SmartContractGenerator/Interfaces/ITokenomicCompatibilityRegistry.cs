using Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;

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
