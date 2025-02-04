using Moongy.RD.Launchpad.Data.Base;

namespace Moongy.RD.LaunchPad.DataAccess.Base.Interfaces;

public interface IBaseDataAccessObject<T> where T : Entity
{
    Task<Guid> CreateAsync(T e);
    Task<T?> GetAsync(Guid id);
    Task<IEnumerable<T>> ListAsync();
    Task UpdateAsync(T e);
    Task DeleteAsync(T e);

    Task<(int, IEnumerable<T>)> ListAsync(int offset, int limit);
}
