using System.Transactions;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Data.Base;
using Moongy.RD.LaunchPad.DataAccess.Base.Interfaces;

namespace Moongy.RD.Launchpad.Business.Base
{
    public abstract class EntityBusinessObject<T>(IBaseDataAccessObject<T> dao) : BaseBusinessObject, IEntityBusinessObject<T> where T : Entity
    {

        public async virtual Task<OperationResult<Guid>> CreateAsync(T entity)
        {
            return await ExecuteOperation(async () =>
            {
                var result = await dao.CreateAsync(entity);
                return result;
            });
        }

        public async Task<OperationResult> DeleteAsync(Guid uuid)
        {
            return await ExecuteOperation(async () =>
            {
                var record = await dao.GetAsync(uuid);
                if (record == null) throw new Exception("Record not found");
                await dao.DeleteAsync(record);
            });
        }

        public async Task<OperationResult<T?>> GetAsync(Guid uuid)
        {
            return await ExecuteOperation(async () =>
            {
                var result = await dao.GetAsync(uuid);
                return result;
            });
        }

        public async Task<OperationResult<IEnumerable<T>>> ListAsync()
        {
            return await ExecuteOperation(async () =>
            {
                var result = await dao.ListAsync();
                return result;
            });

        }

        public  async virtual Task<OperationResult> UpdateAsync(Guid uuid, T entity)
        {
            return await Task.Run(() =>
            {
                return new OperationResult() { Exception = new NotImplementedException() };
            });
        }
    }
}
