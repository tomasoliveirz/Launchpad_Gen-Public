using Moongy.RD.Launchpad.Data.Base;

namespace Moongy.RD.LaunchPad.DataAccess.Base.Interfaces
{
    public interface IGenericDataAccessObject
    {
        Task<Guid> CreateAsync<T>(T e) where T : Entity;
        Task<T?> GetAsync<T>(Guid id) where T : Entity;
        Task<IEnumerable<T>> ListAsync<T>() where T : Entity;
        Task UpdateAsync<T>(T e) where T : Entity;
        Task DeleteAsync<T>(T e) where T : Entity;
        Task<(int, IEnumerable<T>)> ListAsync<T>(int offset, int limit) where T : Entity;
    }
}
