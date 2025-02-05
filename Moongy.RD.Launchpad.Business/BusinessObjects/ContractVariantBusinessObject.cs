using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.Launchpad.Business.Interfaces;
using System.Transactions;
using Microsoft.IdentityModel.Tokens;

namespace Moongy.RD.Launchpad.Business.BusinessObjects;

public class ContractVariantBusinessObject(IContractVariantDataAccessObject dao) : EntityBusinessObject<ContractVariant>(dao), IContractVariantBusinessObject
{
    public override async Task<OperationResult<Guid>> CreateAsync(ContractVariant contractVariant)
    {
        try
        {
            using var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted, Timeout = TimeSpan.FromSeconds(90) }, TransactionScopeAsyncFlowOption.Enabled);
            if (string.IsNullOrEmpty(contractVariant.Name)) throw new Exception("Invalid model exception: name is missing");
            var result = await dao.CreateAsync(contractVariant);
            scope.Complete();
            return new OperationResult<Guid>() { Result = result };
        }
        catch (Exception ex)
        {
            return new OperationResult<Guid>() { Exception = ex };
        }
    }

    public override async Task<OperationResult> UpdateAsync(Guid uuid, ContractVariant contractVariant)
    {
        try
        {
            using var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted, Timeout = TimeSpan.FromSeconds(90) }, TransactionScopeAsyncFlowOption.Enabled);
            if (string.IsNullOrEmpty(contractVariant.Name)) throw new Exception("Invalid model exception: name is missing");
            var oldRecord = await dao.GetAsync(uuid);
            if (oldRecord == null) throw new Exception("Record not found");
            oldRecord.Name = contractVariant.Name;
            oldRecord.Description = contractVariant.Description;
            await dao.UpdateAsync(oldRecord);
            scope.Complete();
            return new OperationResult { };
        }
        catch (Exception ex)
        {
            return new OperationResult() { Exception = ex };
        }
    }
}
