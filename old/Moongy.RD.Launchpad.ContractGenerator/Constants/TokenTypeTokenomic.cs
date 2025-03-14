using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.ContractGenerator.ModelComposers.FungibleToken.Models;
using Moongy.RD.Launchpad.Decorators.Core.Interfaces;
using Moongy.RD.Launchpad.Decorators.Tokenomics.AntiWhale.Models;

namespace Moongy.RD.Launchpad.ContractGenerator.Constants
{
    public static class TokenTypeTokenomic
    {
        private static Dictionary<Type, Type> _tokenomicsOnTokens = new Dictionary<Type, Type>()
        {
            new {Key = typeof(FungibleTokenModel), Value = typeof(AntiWhaleTokenomic)}
        }

        public static bool IsCompatible(IToken token, ITokenomic tokenomic)
        {

        }
    }
}
