using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.LaunchPad.DataAccess.Base.Interfaces;

namespace Moongy.RD.Launchpad.Business.BusinessObjects;

public class ContractVariantBusinessObject(IContractVariantDataAccessObject dao, IGenericDataAccessObject genericDao) : EntityBusinessObject<ContractVariant>(dao), IContractVariantBusinessObject
{
    public  async Task<OperationResult<Guid>> CreateAsync(ContractVariant contractVariant, Guid contractTypeUuid)
    {
        return await ExecuteOperation(async () =>
        {
            if (string.IsNullOrEmpty(contractVariant.Name)) throw new Exception("Invalid model exception: name is missing");
            var contractType = await genericDao.GetAsync<ContractType>(contractTypeUuid);
            if (contractType == null) throw new Exception("Contract Type not found");
            contractVariant.ContractTypeId = contractType.Id;
            var result = await dao.CreateAsync(contractVariant);
            return result;
        });
    }

    public  async Task<OperationResult> UpdateAsync(Guid uuid, ContractVariant contractVariant, Guid? contractTypeUuid)
    {
        return await ExecuteOperation(async () =>
        {
            if (string.IsNullOrEmpty(contractVariant.Name)) throw new Exception("Invalid model exception: name is missing");
            var oldRecord = await dao.GetAsync(uuid);
            if (oldRecord == null) throw new Exception("Record not found");
            oldRecord.Name = contractVariant.Name;
            oldRecord.Description = contractVariant.Description;
            if (contractTypeUuid != null)
            {
                var contractType = await genericDao.GetAsync<ContractType>(contractTypeUuid.Value);
                if (contractType == null) throw new Exception("Contract Type not found");
                oldRecord.ContractTypeId = contractType.Id;
            }
            await dao.UpdateAsync(oldRecord);
        });
    }
}
