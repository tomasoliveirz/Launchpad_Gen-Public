using System.Transactions;
using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;

namespace Moongy.RD.Launchpad.Business.BusinessObjects
{
    public class ContractTypeBusinessObject(IContractTypeDataAccessObject dao) : EntityBusinessObject<ContractType>(dao), IContractTypeBusinessObject
    {
        public override async Task<OperationResult<Guid>> CreateAsync(ContractType contractType)
        {
            try
            {
                using var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted, Timeout = TimeSpan.FromSeconds(90) }, TransactionScopeAsyncFlowOption.Enabled);
                if (string.IsNullOrEmpty(contractType.Name)) throw new Exception("Invalid model exception: name is missing");
                var result = await dao.CreateAsync(contractType);
                scope.Complete();
                return new OperationResult<Guid>() { Result = result };
            }
            catch (Exception ex)
            {
                return new OperationResult<Guid>() { Exception = ex };
            }
        }
 
        public override async Task<OperationResult> UpdateAsync(Guid uuid, ContractType entity)
        {
            try
            {
                using var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted, Timeout = TimeSpan.FromSeconds(90) }, TransactionScopeAsyncFlowOption.Enabled);
                
                if (string.IsNullOrEmpty(entity.Name)) throw new Exception("Invalid model exception: name is missing");
                var oldRecord = await dao.GetAsync(uuid);
                if (oldRecord == null) throw new Exception("Record not found");
                oldRecord.Name = entity.Name;
                oldRecord.Description = entity.Description; 
                await dao.UpdateAsync(oldRecord);
                scope.Complete();
                return new OperationResult() {  };
            }
            catch (Exception ex)
            {
                return new OperationResult() { Exception = ex };
            }
        }
    }
}
