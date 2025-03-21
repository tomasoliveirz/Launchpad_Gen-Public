using Moongy.RD.Launchpad.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;
public interface ITokenomicDecorator<TTokenomic> where TTokenomic : ITokenomic
{
    public SmartContractModel Decorate(TTokenomic tokenomic, SmartContractModel model);
}
