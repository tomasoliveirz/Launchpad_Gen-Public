using Moongy.RD.Launchpad.Core.Models;

namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces
{
    public interface ISmartContractCompiler<TResult, TVersion> where TVersion: SoftwareVersion
    {
        TResult Compile(string name, string sourceCode, TVersion softwareVersion);
    }
}
