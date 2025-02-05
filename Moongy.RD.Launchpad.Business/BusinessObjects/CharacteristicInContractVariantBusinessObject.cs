using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Base.Interfaces;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;

namespace Moongy.RD.Launchpad.Business.BusinessObjects;

public class CharacteristicInContractVariantBusinessObject(ICharacteristicInContractVariantDataAccessObject dao, IGenericDataAccessObject genericDao) : EntityBusinessObject<CharacteristicInContractVariant>(dao), ICharacteristicInContractVariantBusinessObject
{
    public async Task<OperationResult<Guid>> CreateAsync(CharacteristicInContractVariant characteristicInContractVariant, Guid contractVariantUuid, Guid contractCharacteristicUuid)
    {
        return await ExecuteOperation(async () =>
        {
            var contractVariant = await genericDao.GetAsync<ContractVariant>(contractVariantUuid);
            if (contractVariant == null) throw new Exception("Contract Variant not found");

            var contractCharacteristic = await genericDao.GetAsync<ContractCharacteristic>(contractCharacteristicUuid);
            if (contractCharacteristic == null) throw new Exception("Contract Characteristic not found");

            characteristicInContractVariant.ContractVariantId = contractVariant.Id;
            characteristicInContractVariant.ContractCharacteristicId = contractCharacteristic.Id;

            var result = await dao.CreateAsync(characteristicInContractVariant);
            return result;
        });
    }

    public async Task<OperationResult> UpdateAsync(Guid uuid, CharacteristicInContractVariant characteristicInContractVariant, Guid? contractVariantUuid, Guid? contractCharacteristicUuid)
    {
        return await ExecuteOperation(async () =>
        {
            var oldRecord = await dao.GetAsync(uuid);
            if (oldRecord == null) throw new Exception("Record not found");

            if (contractVariantUuid != null)
            {
                var contractVariant = await genericDao.GetAsync<ContractVariant>(contractVariantUuid.Value);
                if (contractVariant == null) throw new Exception("Contract Variant Result not found");
                oldRecord.ContractVariantId = contractVariant.Id;
            }

            if (contractCharacteristicUuid != null)
            {
                var contractCharacteristic = await genericDao.GetAsync<BlockchainNetwork>(contractCharacteristicUuid.Value);
                if (contractCharacteristic == null) throw new Exception("Blockchain Network Result not found");
                oldRecord.ContractCharacteristicId = contractCharacteristic.Id;
            }
            await dao.UpdateAsync(oldRecord);
        });
    }
}
