using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Data.Base;

namespace Moongy.RD.Launchpad.Business.Interfaces
{
    public interface IEntityBusinessObject<T> where T : Entity
    {
        public Task<OperationResult<IEnumerable<T>>> ListAsync();
        public Task<OperationResult<T?>> GetAsync(Guid uuid);
        public Task<OperationResult<Guid>> CreateAsync(T entity);
        public Task<OperationResult> UpdateAsync(Guid uuid, T entity);
        public Task<OperationResult> DeleteAsync(Guid uuid);
    }
}
