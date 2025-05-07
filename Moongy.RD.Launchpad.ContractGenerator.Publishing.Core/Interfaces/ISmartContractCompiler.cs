
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Header;

namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces
{
    public interface ISmartContractCompiler<TResult, TVersion> where TVersion: VersionModel
    {
        TResult Compile(string name, string sourceCode, TVersion softwareVersion);
    }
}
