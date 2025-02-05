using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Moongy.RD.Launchpad.Business.BusinessObjects;

public class ContractFeatureGroupBusinessObject(IContractFeatureGroupDataAccessObject dao) : EntityBusinessObject<ContractFeatureGroup>(dao), IContractFeatureGroupBusinessObject
{
    public override async Task<OperationResult<Guid>> CreateAsync(ContractFeatureGroup contractFeatureGroup)
    {
        try
        {
            using var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted, Timeout = TimeSpan.FromSeconds(90) }, TransactionScopeAsyncFlowOption.Enabled);
            if (string.IsNullOrEmpty(contractFeatureGroup.Name)) throw new Exception("Invalid model exception: name is missing");
            if (string.IsNullOrEmpty(contractFeatureGroup.DataType)) throw new Exception("Invalid model exception: dataType is missing");
            var result = await dao.CreateAsync(contractFeatureGroup);
            scope.Complete();
            return new OperationResult<Guid>() { Result = result };
        }
        catch (Exception ex)
        {
            return new OperationResult<Guid>() { Exception = ex };
        }
    }

    public override async Task<OperationResult> UpdateAsync(Guid uuid, ContractFeatureGroup contractFeatureGroup)
    {
        try
        {
            using var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted, Timeout = TimeSpan.FromSeconds(90) }, TransactionScopeAsyncFlowOption.Enabled);

            if (string.IsNullOrEmpty(contractFeatureGroup.Name)) throw new Exception("Invalid model exception: name is missing");
            var oldRecord = await dao.GetAsync(uuid);
            if (oldRecord == null) throw new Exception("Record not found");
            oldRecord.Name = contractFeatureGroup.Name;
            oldRecord.Description = contractFeatureGroup.Description;
            oldRecord.DataType = contractFeatureGroup.DataType;
            await dao.UpdateAsync(oldRecord);
            scope.Complete();
            return new OperationResult() { };
        }
        catch (Exception ex)
        {
            return new OperationResult() { Exception = ex };
        }
    }

}
