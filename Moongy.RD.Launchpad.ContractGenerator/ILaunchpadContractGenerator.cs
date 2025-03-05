using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.ContractGenerator.Enums;
using Moongy.RD.Launchpad.ContractGenerator.ModelComposers.FungibleToken.Models;
using Moongy.RD.Launchpad.Decorators.Core.Interfaces;

namespace Moongy.RD.Launchpad.ContractGenerator
{
    public interface ILaunchpadContractGenerator
    {
        string Generate(FungibleTokenModel fungibleToken, List<ITokenomic> tokenomics, LanguageEnum language);
    }
}
