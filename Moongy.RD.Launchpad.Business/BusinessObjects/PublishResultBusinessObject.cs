using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Base.Interfaces;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;

namespace Moongy.RD.Launchpad.Business.BusinessObjects;

public class PublishResultBusinessObject(IPublishResultDataAccessObject dao, IGenericDataAccessObject genericDao) : EntityBusinessObject<PublishResult>(dao), IPublishResultBusinessObject
{
    public async Task<OperationResult<Guid>> CreateAsync(PublishResult publishResult, Guid contractGenerationResultUuid, Guid blockchainNetworkUuid)
    {
        return await ExecuteOperation(async () =>
        {
            if (string.IsNullOrEmpty(publishResult.Address)) throw new Exception("Invalid model exception: address is missing");
            if (string.IsNullOrEmpty(publishResult.Bytecode)) throw new Exception("Invalid model exception: bytecode is missing");
            if (string.IsNullOrEmpty(publishResult.Abi)) throw new Exception("Invalid model exception: abi is missing");
            var contractGenerationResult = await genericDao.GetAsync<ContractGenerationResult>(contractGenerationResultUuid);
            if (contractGenerationResult == null) throw new Exception("Contract Generation Result not found");

            var blockchainNetwork = await genericDao.GetAsync<BlockchainNetwork>(blockchainNetworkUuid);
            if (blockchainNetwork == null) throw new Exception("Blockchain Network not found");

            publishResult.ContractGenerationResultId = contractGenerationResult.Id;
            publishResult.BlockchainNetworkId = blockchainNetwork.Id;

            var result = await dao.CreateAsync(publishResult);
            return result;
        });
    }

    public async Task<OperationResult> UpdateAsync(Guid uuid, PublishResult publishResult, Guid? contractGenerationResultUuid, Guid? blockchainNetworkUuid)
    {
        return await ExecuteOperation(async () =>
        {
            if (string.IsNullOrEmpty(publishResult.Address)) throw new Exception("Invalid model exception: address is missing");
            if (string.IsNullOrEmpty(publishResult.Bytecode)) throw new Exception("Invalid model exception: bytecode is missing");
            if (string.IsNullOrEmpty(publishResult.Abi)) throw new Exception("Invalid model exception: abi is missing");
            var oldRecord = await dao.GetAsync(uuid);
            if (oldRecord == null) throw new Exception("Record not found");
            oldRecord.Address = publishResult.Address;
            oldRecord.Bytecode = publishResult.Bytecode;
            oldRecord.Abi = publishResult.Abi;

            if (contractGenerationResultUuid != null)
            {
                var contractGenerationResult = await genericDao.GetAsync<ContractGenerationResult>(contractGenerationResultUuid.Value);
                if (contractGenerationResult == null) throw new Exception("Contract Generation Result not found");
                oldRecord.ContractGenerationResultId = contractGenerationResult.Id;
            }

            if (blockchainNetworkUuid != null)
            {
                var blockchainNetwork = await genericDao.GetAsync<BlockchainNetwork>(blockchainNetworkUuid.Value);
                if (blockchainNetwork == null) throw new Exception("Blockchain Network Result not found");
                oldRecord.BlockchainNetworkId = blockchainNetwork.Id;
            }
            await dao.UpdateAsync(oldRecord);
        });
    }
}
