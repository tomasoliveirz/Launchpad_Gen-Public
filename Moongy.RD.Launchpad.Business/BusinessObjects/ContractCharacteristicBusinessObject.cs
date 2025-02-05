using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;

namespace Moongy.RD.Launchpad.Business.BusinessObjects;

public class ContractCharacteristicBusinessObject(IContractCharacteristicDataAccessObject dao) : EntityBusinessObject<ContractCharacteristic>(dao), IContractCharacteristicBusinessObject
{
    public override async Task<OperationResult<Guid>> CreateAsync(ContractCharacteristic contractCharacteristic)
    {
        return await ExecuteOperation(async () =>
        {
            if (string.IsNullOrEmpty(contractCharacteristic.Name)) throw new Exception("Invalid model exception: name is missing");
            var result = await dao.CreateAsync(contractCharacteristic);
            return result;
        });
    }

    public override async Task<OperationResult> UpdateAsync(Guid uuid, ContractCharacteristic contractCharacteristic)
    {
        return await ExecuteOperation(async () =>
        {
            if (string.IsNullOrEmpty(contractCharacteristic.Name)) throw new Exception("Invalid model exception: name is missing");
            var oldRecord = await dao.GetAsync(uuid);
            if (oldRecord == null) throw new Exception("Record not found");
            oldRecord.Name = contractCharacteristic.Name;
            oldRecord.Description = contractCharacteristic.Description;
            await dao.UpdateAsync(oldRecord);
        });
    }
}
