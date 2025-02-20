using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Business.Exceptions;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Base.Interfaces;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;

namespace Moongy.RD.Launchpad.Business.BusinessObjects;

public class ContractGenerationResultBusinessObject(IContractGenerationResultDataAccessObject dao, IGenericDataAccessObject genericDao) : EntityBusinessObject<ContractGenerationResult>(dao, genericDao), IContractGenerationResultBusinessObject
{
    public async Task<OperationResult<Guid>> CreateAsync(ContractGenerationResult contractGenerationResult, Guid contractVariantUuid)
    {
        return await ExecuteOperation(async () =>
        {
            if (string.IsNullOrEmpty(contractGenerationResult.Name)) throw new InvalidModelException("name is missing");
            if (string.IsNullOrEmpty(contractGenerationResult.Description)) throw new InvalidModelException("description is missing");
            contractGenerationResult.CreateAt = DateOnly.FromDateTime(DateTime.Now);

            var contractVariant = await genericDao.GetAsync<ContractVariant>(contractVariantUuid) ?? throw new NotFoundException("Contract Variant", contractVariantUuid.ToString());
            contractGenerationResult.ContractVariantId = contractVariant.Id;

            var result = await dao.CreateAsync(contractGenerationResult);
            return result;
        });
    }

    public async Task<OperationResult> UpdateAsync(Guid uuid, ContractGenerationResult contractGenerationResult, Guid? contractVariantUuid)
    {
        return await ExecuteOperation(async () =>
        {
            if (string.IsNullOrEmpty(contractGenerationResult.Name)) throw new InvalidModelException("name is missing");
            if (string.IsNullOrEmpty(contractGenerationResult.Description)) throw new InvalidModelException("description is missing");
            var oldRecord = await dao.GetAsync(uuid) ?? throw new NotFoundException("Contract Generation Result", uuid.ToString());
            oldRecord.Name = contractGenerationResult.Name;
            oldRecord.Description = contractGenerationResult.Description;

            if (contractVariantUuid != null)
            {
                var contractVariant = await genericDao.GetAsync<ContractVariant>(contractVariantUuid.Value) ?? throw new NotFoundException("Contract Variant", contractVariantUuid.Value.ToString());
                oldRecord.ContractVariantId = contractVariant.Id;
            }
            await dao.UpdateAsync(oldRecord);
        });
    }
}
