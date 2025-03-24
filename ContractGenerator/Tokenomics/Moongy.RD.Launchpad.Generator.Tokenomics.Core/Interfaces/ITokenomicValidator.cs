using Moongy.RD.Launchpad.Core.Interfaces;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;
public interface ITokenomicValidator<TTokenomic> : IValidator<TTokenomic> where TTokenomic : ITokenomic
{
}
