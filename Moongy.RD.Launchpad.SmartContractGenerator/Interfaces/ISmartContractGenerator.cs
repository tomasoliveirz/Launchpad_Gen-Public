using Moongy.RD.Launchpad.ContractGenerator.Contracts.AdvancedFungibleToken.Models;
using Moongy.RD.Launchpad.ContractGenerator.Contracts.FungibleToken.Models;
using Moongy.RD.Launchpad.ContractGenerator.Contracts.NonFungibleToken.Models;
using Moongy.RD.Launchpad.ContractGenerator.Contracts.SemiFungibleToken.Models;
using Moongy.RD.Launchpad.ContractGenerator.Tokenomics.Core.Interfaces;
using Moongy.RD.Launchpad.SmartContractGenerator.Enums;
using Moongy.RD.Launchpad.SmartContractGenerator.Models;

namespace Moongy.RD.Launchpad.SmartContractGenerator.Interfaces;
public interface ISmartContractGenerator
{
    public GenerationResult<FungibleTokenModel> Generate(FungibleTokenModel model, List<ITokenomic> tokenomics, SmartContractVirtualMachine vm);     
    public GenerationResult<AdvancedFungibleTokenModel> Generate(AdvancedFungibleTokenModel model, List<ITokenomic> tokenomics, SmartContractVirtualMachine vm);     
    public GenerationResult<NonFungibleTokenModel> Generate(NonFungibleTokenModel model, List<ITokenomic> tokenomics, SmartContractVirtualMachine vm);
    public GenerationResult<SemiFungibleTokenModel> Generate(SemiFungibleTokenModel model, List<ITokenomic> tokenomics, SmartContractVirtualMachine vm);
}
