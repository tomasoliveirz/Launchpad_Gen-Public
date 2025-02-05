using System.Transactions;

namespace Moongy.RD.Launchpad.Business.Base
{
    public class BaseBusinessObject()
    {
        public async Task<OperationResult<TResult>> ExecuteOperation<TResult>(Func<Task<TResult>> operation, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, long timeOut = 90)
        {
            try
            {
                using var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = isolationLevel, Timeout = TimeSpan.FromSeconds(timeOut) }, TransactionScopeAsyncFlowOption.Enabled);
                var result = await operation.Invoke();
                scope.Complete();
                return new OperationResult<TResult>() { Result = result };
            }
            catch (Exception ex)
            {
                return new OperationResult<TResult>() { Exception = ex };
            }
        }

        public async Task<OperationResult> ExecuteOperation(Func<Task> operation, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, long timeOut = 90)
        {
            try
            {
                using var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = isolationLevel, Timeout = TimeSpan.FromSeconds(timeOut) }, TransactionScopeAsyncFlowOption.Enabled);
                await operation.Invoke();
                scope.Complete();
                return new OperationResult() { };
            }
            catch (Exception ex)
            {
                return new OperationResult() { Exception = ex };
            }
        }
    }
}
