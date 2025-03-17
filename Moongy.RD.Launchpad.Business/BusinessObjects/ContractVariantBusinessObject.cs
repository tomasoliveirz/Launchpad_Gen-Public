using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.LaunchPad.DataAccess.Base.Interfaces;
using Moongy.RD.Launchpad.Business.Exceptions;
namespace Moongy.RD.Launchpad.Business.BusinessObjects;

public class ContractVariantBusinessObject(IContractVariantDataAccessObject dao, IGenericDataAccessObject genericDao) : EntityBusinessObject<ContractVariant>(dao, genericDao), IContractVariantBusinessObject
{
    public  async Task<OperationResult<Guid>> CreateAsync(ContractVariant contractVariant, Guid contractTypeUuid)
    {
        return await ExecuteOperation(async () =>
        {
            if (string.IsNullOrEmpty(contractVariant.Name)) throw new InvalidModelException("name is missing");
            contractVariant = await FindAndAttach(contractVariant, contractTypeUuid, x => x.ContractType, x => x.ContractTypeId);
            var result = await dao.CreateAsync(contractVariant);
            return result;
        });
    }

    public  async Task<OperationResult> UpdateAsync(Guid uuid, ContractVariant contractVariant, Guid? contractTypeUuid)
    {
        return await ExecuteOperation(async () =>
        {
            if (string.IsNullOrEmpty(contractVariant.Name)) throw new InvalidModelException("name is missing");
            var oldRecord = await dao.GetAsync(uuid) ?? throw new NotFoundException("Contract Variant", uuid.ToString());
            oldRecord.Name = contractVariant.Name;
            oldRecord.Description = contractVariant.Description;
            if (contractTypeUuid != null)
            {
                var contractType = await genericDao.GetAsync<ContractType>(contractTypeUuid.Value) ?? throw new NotFoundException("Contract Type", contractTypeUuid.Value.ToString());
                oldRecord.ContractTypeId = contractType.Id;
            }
            await dao.UpdateAsync(oldRecord);
        });
    }

    public async Task<OperationResult<IEnumerable<ContractVariant>>> GetVariantsWithTypes()
    {
        return await ExecuteOperation(async () =>
        {
            var records = await dao.GetContractVariantsWithType();
            return records;
        });
    }

    public async Task<OperationResult<ContractVariant>> GetVariantWithType(Guid contractVariantUuid)
    {
        return await ExecuteOperation(async () =>
        {
            var records = await dao.GetContractVariantWithType(contractVariantUuid);
            return records;
        });
    }

    public async Task<OperationResult<IEnumerable<CharacteristicInContractVariant>>> GetCharacteristicsInContractVariant(Guid contractVariantUuid)
    {
        return await ExecuteOperation(async () =>
        {
            var records = await dao.GetCharacteristicsInContractVariant(contractVariantUuid);
            return records;
        });
    }
}
