using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Base.Interfaces;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;

namespace Moongy.RD.Launchpad.Business.BusinessObjects;

public class ContractFeatureGroupBusinessObject(IContractFeatureGroupDataAccessObject dao, IGenericDataAccessObject genericDataAccessObject) : EntityBusinessObject<ContractFeatureGroup>(dao), IContractFeatureGroupBusinessObject
{
    public  async Task<OperationResult<Guid>> CreateAsync(ContractFeatureGroup contractFeatureGroup, Guid generationResultUuid)
    {
        return await ExecuteOperation(async () => {
            if (string.IsNullOrEmpty(contractFeatureGroup.Name)) throw new Exception("Invalid model exception: name is missing");
            if (string.IsNullOrEmpty(contractFeatureGroup.DataType)) throw new Exception("Invalid model exception: dataType is missing");
            var generationResult = await genericDataAccessObject.GetAsync<ContractGenerationResult>(generationResultUuid);
            if (generationResult == null) throw new Exception("Generation does not exist");
            contractFeatureGroup.ContractGenerationResultId = generationResult.Id;
            var result = await dao.CreateAsync(contractFeatureGroup);
            return result;
        });
    }

    public  async Task<OperationResult> UpdateAsync(Guid uuid, ContractFeatureGroup contractFeatureGroup, Guid? generationResultUuid)
    {
        return await ExecuteOperation(async () =>
        {
            if (string.IsNullOrEmpty(contractFeatureGroup.Name)) throw new Exception("Invalid model exception: name is missing");
            var oldRecord = await dao.GetAsync(uuid);
            if (oldRecord == null) throw new Exception("Record not found");
            oldRecord.Name = contractFeatureGroup.Name;
            oldRecord.Description = contractFeatureGroup.Description;
            oldRecord.DataType = contractFeatureGroup.DataType;
            if(generationResultUuid != null)
            {
                var generationResult = await genericDataAccessObject.GetAsync<ContractGenerationResult>(generationResultUuid.Value);
                if (generationResult == null) throw new Exception("Generation does not exist");
                oldRecord.ContractGenerationResultId = generationResult.Id;
            }
            await dao.UpdateAsync(oldRecord);
        });
    }

}
