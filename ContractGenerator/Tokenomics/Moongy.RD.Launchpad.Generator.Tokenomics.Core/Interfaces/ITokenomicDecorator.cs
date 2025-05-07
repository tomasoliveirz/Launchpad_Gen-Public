
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;
public interface ITokenomicDecorator<TTokenomic> where TTokenomic : ITokenomic
{
    public SolidityContractModel Decorate(TTokenomic tokenomic, SolidityContractModel model);
}
