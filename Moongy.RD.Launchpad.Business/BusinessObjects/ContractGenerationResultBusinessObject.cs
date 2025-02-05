using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Base.Interfaces;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;

namespace Moongy.RD.Launchpad.Business.BusinessObjects;

public class ContractGenerationResultBusinessObject(IContractGenerationResultDataAccessObject dao, IGenericDataAccessObject genericDao) : EntityBusinessObject<ContractGenerationResult>(dao), IContractGenerationResultBusinessObject
{
    public async Task<OperationResult<Guid>> CreateAsync(ContractGenerationResult contractGenerationResult, Guid contractVariantUuid)
    {
        return await ExecuteOperation(async () =>
        {
            if (string.IsNullOrEmpty(contractGenerationResult.Name)) throw new Exception("Invalid model exception: name is missing");
            if (string.IsNullOrEmpty(contractGenerationResult.Description)) throw new Exception("Invalid model exception: description is missing");
            contractGenerationResult.CreateAt = DateOnly.FromDateTime(DateTime.Now);

            var contractVariant = await genericDao.GetAsync<ContractVariant>(contractVariantUuid);
            if (contractVariant == null) throw new Exception("Contract Variant not found");
            contractGenerationResult.ContractVariantId = contractVariant.Id;

            var result = await dao.CreateAsync(contractGenerationResult);
            return result;
        });
    }

    public async Task<OperationResult> UpdateAsync(Guid uuid, ContractGenerationResult contractGenerationResult, Guid? contractVariantUuid)
    {
        return await ExecuteOperation(async () =>
        {
            if (string.IsNullOrEmpty(contractGenerationResult.Name)) throw new Exception("Invalid model exception: name is missing");
            if (string.IsNullOrEmpty(contractGenerationResult.Description)) throw new Exception("Invalid model exception: description is missing");
            var oldRecord = await dao.GetAsync(uuid);
            if (oldRecord == null) throw new Exception("Record not found");
            oldRecord.Name = contractGenerationResult.Name;
            oldRecord.Description = contractGenerationResult.Description;

            if (contractVariantUuid != null)
            {
                var contractVariant = await genericDao.GetAsync<ContractVariant>(contractVariantUuid.Value);
                if (contractVariant == null) throw new Exception("Contract Variant not found");
                oldRecord.ContractVariantId = contractVariant.Id;
            }
            await dao.UpdateAsync(oldRecord);
        });
    }
}
