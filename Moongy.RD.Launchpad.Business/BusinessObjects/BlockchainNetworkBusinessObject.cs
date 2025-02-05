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

public class BlockchainNetworkBusinessObject(IBlockchainNetworkDataAccessObject dao) : EntityBusinessObject<BlockchainNetwork>(dao), IBlockchainNetworkBusinessObject
{
    public override async Task<OperationResult<Guid>> CreateAsync(BlockchainNetwork blockchainNetwork)
    {
        try
        {
            using var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted, Timeout = TimeSpan.FromSeconds(90) }, TransactionScopeAsyncFlowOption.Enabled);
            if (string.IsNullOrEmpty(blockchainNetwork.Name)) throw new Exception("Invalid model exception: name is missing");
            var result = await dao.CreateAsync(blockchainNetwork);
            scope.Complete();
            return new OperationResult<Guid>() { Result = result };
        }
        catch (Exception ex)
        {
            return new OperationResult<Guid>() { Exception = ex };
        }
    }

    public override async Task<OperationResult> UpdateAsync(Guid uuid, BlockchainNetwork blockchainNetwork)
    {
        try
        {
            using var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted, Timeout = TimeSpan.FromSeconds(90) }, TransactionScopeAsyncFlowOption.Enabled);
            if (string.IsNullOrEmpty(blockchainNetwork.Name)) throw new Exception("Invalid model exception: name is missing");
            var oldRecord = await dao.GetAsync(uuid);
            if (oldRecord == null) throw new Exception("Record not found");
            oldRecord.Name = blockchainNetwork.Name;
            oldRecord.Description = blockchainNetwork.Description;
            oldRecord.Image = blockchainNetwork.Image;
            await dao.UpdateAsync(oldRecord);
            scope.Complete();
            return new OperationResult() { };

        }
        catch (Exception ex)
        {
            return new OperationResult<Guid>() { Exception = ex };
        }
    }
}
