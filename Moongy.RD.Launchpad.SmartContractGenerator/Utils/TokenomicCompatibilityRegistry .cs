using Moongy.RD.Launchpad.ContractGenerator.Contracts.Core.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.Tokenomics.Core.Interfaces;
using Moongy.RD.Launchpad.SmartContractGenerator.Interfaces;

namespace Moongy.RD.Launchpad.SmartContractGenerator.Utils
{
    public class TokenomicCompatibilityRegistry : ITokenomicCompatibilityRegistry
    {
        private readonly Dictionary<Type, HashSet<Type>> _tokenToTokenomics = [];
        private readonly Dictionary<Type, HashSet<Type>> _tokenomicToTokens = [];


        public List<Type> GetCompatibleTokenomics<TToken>() where TToken : IToken
        {
            return _tokenToTokenomics.TryGetValue(typeof(TToken), out var compatibleTokenomics)
            ? [.. compatibleTokenomics]
            : [];
        }

        public List<Type> GetCompatibleTokens<TTokenomic>() where TTokenomic : ITokenomic
        {
            return _tokenomicToTokens.TryGetValue(typeof(TTokenomic), out var compatibleTokens)
            ? [.. compatibleTokens]
            : [];
        }

        public bool IsCompatible<TToken, TTokenomic>()
            where TToken : IToken
            where TTokenomic : ITokenomic
        {
            return _tokenToTokenomics.TryGetValue(typeof(TToken), out var compatibleTokenomics) &&
               compatibleTokenomics.Contains(typeof(TTokenomic));
        }

        public void RegisterCompatibility<TToken, TTokenomic>()
            where TToken : IToken
            where TTokenomic : ITokenomic
        {
            var tokenType = typeof(TToken);
            var tokenomicType = typeof(TTokenomic);

            if (!_tokenToTokenomics.TryGetValue(tokenType, out HashSet<Type>? value))
            {
                value = [];
                _tokenToTokenomics[tokenType] = value;
            }

            value.Add(tokenomicType);

            if (!_tokenomicToTokens.TryGetValue(tokenomicType, out HashSet<Type>? tvalue))
            {
                tvalue = [];
                _tokenomicToTokens[tokenomicType] = tvalue;
            }

            tvalue.Add(tokenType);
        }
    }
}
