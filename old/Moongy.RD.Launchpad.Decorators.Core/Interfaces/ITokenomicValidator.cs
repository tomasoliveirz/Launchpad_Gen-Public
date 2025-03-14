
using Moongy.RD.Launchpad.Core.Interfaces;

namespace Moongy.RD.Launchpad.Decorators.Core.Interfaces;

public interface ITokenomicValidator<T> : IValidator<T> where T:ITokenomic
{

}
