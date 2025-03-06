using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Business.Exceptions;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Base.Interfaces;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;

namespace Moongy.RD.Launchpad.Business.BusinessObjects;

public class CharacteristicInContractVariantBusinessObject(ICharacteristicInContractVariantDataAccessObject dao, IGenericDataAccessObject genericDao) : EntityBusinessObject<CharacteristicInContractVariant>(dao, genericDao), ICharacteristicInContractVariantBusinessObject
{
    public async Task<OperationResult<Guid>> CreateAsync(CharacteristicInContractVariant characteristicInContractVariant, Guid contractVariantUuid, Guid contractCharacteristicUuid)
    {
        return await ExecuteOperation(async () =>
        {
            //var contractVariant = await genericDao.GetAsync<ContractVariant>(contractVariantUuid) ?? throw new Exception("Contract Variant not found");
            //var contractCharacteristic = await genericDao.GetAsync<ContractCharacteristic>(contractCharacteristicUuid) ?? throw new Exception("Contract Characteristic not found");
            //characteristicInContractVariant.ContractVariantId = contractVariant.Id;
            //characteristicInContractVariant.ContractCharacteristicId = contractCharacteristic.Id;
            characteristicInContractVariant = await FindAndAttach(characteristicInContractVariant, contractVariantUuid, x => x.ContractVariant, x => x.ContractVariantId);
            characteristicInContractVariant = await FindAndAttach(characteristicInContractVariant, contractCharacteristicUuid, x => x.ContractCharacteristic, x => x.ContractCharacteristicId);
            characteristicInContractVariant.Uuid = null;
            var result = await dao.CreateAsync(characteristicInContractVariant);
            return result;
        });
    }

    public async Task<OperationResult> UpdateAsync(Guid uuid, CharacteristicInContractVariant characteristicInContractVariant, Guid? contractVariantUuid, Guid? contractCharacteristicUuid)
    {
        return await ExecuteOperation(async () =>
        {
            var oldRecord = await dao.GetAsync(uuid) ?? throw new NotFoundException("Characteristic in Contract Variant", uuid.ToString());
            if (contractVariantUuid != null)
            {
                var contractVariant = await genericDao.GetAsync<ContractVariant>(contractVariantUuid.Value) ?? throw new NotFoundException("Contract Variant", uuid.ToString());
                oldRecord.ContractVariantId = contractVariant.Id;
            }

            if (contractCharacteristicUuid != null)
            {
                var contractCharacteristic = await genericDao.GetAsync<ContractCharacteristic>(contractCharacteristicUuid.Value) ?? throw new NotFoundException("Contract Characteristic", uuid.ToString());
                oldRecord.ContractCharacteristicId = contractCharacteristic.Id;
            }
            await dao.UpdateAsync(oldRecord);
        });
    }

    public async Task<OperationResult<IEnumerable<CharacteristicInContractVariant>>> GetCharacteristicsInContractVariants()
    {
        return await ExecuteOperation(async () =>
        {
            var records = await dao.GetCharacteristicsInContractVariants();
            return records;
        });
    }

    public async Task<OperationResult<CharacteristicInContractVariant>> GetCharacteristicInContractVariant(Guid contractCharacteristicUuid)
    {
        return await ExecuteOperation(async () =>
        {
            var records = await dao.GetCharacteristicInContractVariant(contractCharacteristicUuid);
            return records;
        });
    }
}
