using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.ContractGenerator.Enums;
using Moongy.RD.Launchpad.ContractGenerator.ModelComposers.FungibleToken.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.ModelComposers.FungibleToken.Models;
using Moongy.RD.Launchpad.Decorators.Core.Interfaces;
using Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Models;

namespace Moongy.RD.Launchpad.ContractGenerator
{
    public class LaunchpadContractGenerator(
        IFungibleTokenComposer fungibleTokenComposer,
        TokenomicDecorator tokenomicDecorator
        ) : ILaunchpadContractGenerator
    {

        public string Generate(FungibleTokenModel fungibleToken, List<ITokenomic> tokenomics, LanguageEnum language)
        {
            var resultModel = fungibleTokenComposer.Compose(fungibleToken);
            foreach (var tokenomic in tokenomics)
            {
                resultModel = tokenomicDecorator.Decorate(tokenomic, resultModel);
            }
        }
    }

    internal class TokenomicDecorator(IAntiWhaleTokenomicDecorator antiWhaleDecorator)
    {
        public SmartContractModel Decorate(ITokenomic tokenomic, SmartContractModel resultModel)
        {
            if (tokenomic is IAntiWhaleTokenomic antiWhaleTokenomic)
            {
                resultModel = antiWhaleDecorator.Decorate(tokenomic, resultModel);
            }
        }

    }
}

