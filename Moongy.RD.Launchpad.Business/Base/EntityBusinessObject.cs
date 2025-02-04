using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Data.Base;
using Moongy.RD.LaunchPad.DataAccess.Base.Interfaces;

namespace Moongy.RD.Launchpad.Business.Base
{
    public abstract class EntityBusinessObject<T>(IBaseDataAccessObject<T> dao) : IEntityBusinessObject<T> where T : Entity
    {
        public async virtual Task<OperationResult<Guid>> CreateAsync(T entity)
        {
            try
            {
                using var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted, Timeout = TimeSpan.FromSeconds(90) }, TransactionScopeAsyncFlowOption.Enabled);
                var result = await dao.CreateAsync(entity);
                scope.Complete();
                return new OperationResult<Guid>() { Result = result };
            }
            catch (Exception ex)
            {
                return new OperationResult<Guid>() { Exception = ex };
            }
        }

        public async Task<OperationResult> DeleteAsync(Guid uuid)
        {
            try
            {
                using var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted, Timeout = TimeSpan.FromSeconds(90) }, TransactionScopeAsyncFlowOption.Enabled);
                var record = await dao.GetAsync(uuid);
                if (record == null) throw new Exception("Record not found");
                 await dao.DeleteAsync(record);
                scope.Complete();
                return new OperationResult() {  };
            }
            catch (Exception ex)
            {
                return new OperationResult() { Exception = ex };
            }
        }

        public async Task<OperationResult<T?>> GetAsync(Guid uuid)
        {
            try
            {
                using var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted, Timeout = TimeSpan.FromSeconds(90) }, TransactionScopeAsyncFlowOption.Enabled);
                var result = await dao.GetAsync(uuid);
                scope.Complete();
                return new OperationResult<T?>() { Result = result };
            }
            catch (Exception ex)
            {
                return new OperationResult<T?>() { Exception = ex };
            }
        }

        public async Task<OperationResult<IEnumerable<T>>> ListAsync()
        {
            try
            {
                using var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted, Timeout = TimeSpan.FromSeconds(90) }, TransactionScopeAsyncFlowOption.Enabled);
                var result = await dao.ListAsync();
                scope.Complete();
                return new OperationResult<IEnumerable<T>>() { Result = result };
            }
            catch (Exception ex)
            {
                return new OperationResult<IEnumerable<T>>() { Exception = ex };
            }
        }

        public  abstract Task<OperationResult> UpdateAsync(Guid uuid, T entity);
    }
}
