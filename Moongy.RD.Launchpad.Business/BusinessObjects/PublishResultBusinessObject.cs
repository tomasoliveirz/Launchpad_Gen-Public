using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Business.Exceptions;
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
            if (string.IsNullOrEmpty(publishResult.Address)) throw new InvalidModelException(" address is missing");
            if (string.IsNullOrEmpty(publishResult.Bytecode)) throw new InvalidModelException(" bytecode is missing");
            if (string.IsNullOrEmpty(publishResult.Abi)) throw new InvalidModelException(" abi is missing");
            var contractGenerationResult = await genericDao.GetAsync<ContractGenerationResult>(contractGenerationResultUuid) ?? throw new NotFoundException("Contract Generation Result", contractGenerationResultUuid.ToString());
            var blockchainNetwork = await genericDao.GetAsync<BlockchainNetwork>(blockchainNetworkUuid) ?? throw new NotFoundException("Blockchain Network", blockchainNetworkUuid.ToString());
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
            if (string.IsNullOrEmpty(publishResult.Address)) throw new InvalidModelException(" address is missing");
            if (string.IsNullOrEmpty(publishResult.Bytecode)) throw new InvalidModelException(" bytecode is missing");
            if (string.IsNullOrEmpty(publishResult.Abi)) throw new InvalidModelException(" abi is missing");
            var oldRecord = await dao.GetAsync(uuid) ?? throw new NotFoundException("Publishing result", uuid.ToString());
            oldRecord.Address = publishResult.Address;
            oldRecord.Bytecode = publishResult.Bytecode;
            oldRecord.Abi = publishResult.Abi;

            if (contractGenerationResultUuid != null)
            {
                var contractGenerationResult = await genericDao.GetAsync<ContractGenerationResult>(contractGenerationResultUuid.Value) ?? throw new NotFoundException("Contract Generation Result", contractGenerationResultUuid.Value.ToString());
                oldRecord.ContractGenerationResultId = contractGenerationResult.Id;
            }

            if (blockchainNetworkUuid != null)
            {
                var blockchainNetwork = await genericDao.GetAsync<BlockchainNetwork>(blockchainNetworkUuid.Value) ?? throw new NotFoundException("Blockchain Network", blockchainNetworkUuid.Value.ToString());
                oldRecord.BlockchainNetworkId = blockchainNetwork.Id;
            }
            await dao.UpdateAsync(oldRecord);
        });
    }
}
