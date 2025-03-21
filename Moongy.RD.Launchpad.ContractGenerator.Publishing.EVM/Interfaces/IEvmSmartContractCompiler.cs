using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.EVM.Models;

namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.EVM.Interfaces
{
    public interface IEvmSmartContractCompiler : ISmartContractCompiler<EvmCompileResult, SolidityVersion>
    {

    }
}
