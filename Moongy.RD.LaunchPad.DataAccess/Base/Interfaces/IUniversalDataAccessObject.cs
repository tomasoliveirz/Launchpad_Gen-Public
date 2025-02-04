namespace Moongy.RD.LaunchPad.DataAccess.Base.Interfaces;

public interface IUniversalDataAccessObject<T>
{
    Task<Guid> CreateAsync(T e);
    Task<T?> GetAsync(Guid id);
    Task<IEnumerable<T>> ListAsync();
    Task UpdateAsync(T e);
    Task DeleteAsync(T e);

    Task<(int, IEnumerable<T>)> ListAsync(int offset, int limit);
}
