using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Business.Exceptions;
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
            if (string.IsNullOrEmpty(contractFeatureGroup.Name)) throw new InvalidModelException("name is missing");
            if (string.IsNullOrEmpty(contractFeatureGroup.DataType)) throw new InvalidModelException("dataType is missing");
            var generationResult = await genericDataAccessObject.GetAsync<ContractGenerationResult>(generationResultUuid) ?? throw new NotFoundException("Generation Result", generationResultUuid.ToString());
            contractFeatureGroup.ContractGenerationResultId = generationResult.Id;
            var result = await dao.CreateAsync(contractFeatureGroup);
            return result;
        });
    }

    public  async Task<OperationResult> UpdateAsync(Guid uuid, ContractFeatureGroup contractFeatureGroup, Guid? generationResultUuid)
    {
        return await ExecuteOperation(async () =>
        {
            if (string.IsNullOrEmpty(contractFeatureGroup.Name)) throw new InvalidModelException("name is missing");
            var oldRecord = await dao.GetAsync(uuid) ?? throw new NotFoundException("Contract Feature Group", uuid.ToString());
            oldRecord.Name = contractFeatureGroup.Name;
            oldRecord.Description = contractFeatureGroup.Description;
            oldRecord.DataType = contractFeatureGroup.DataType;
            if(generationResultUuid != null)
            {
                var generationResult = await genericDataAccessObject.GetAsync<ContractGenerationResult>(generationResultUuid.Value) ?? throw new NotFoundException("Generation Result", generationResultUuid.Value.ToString());
                oldRecord.ContractGenerationResultId = generationResult.Id;
            }
            await dao.UpdateAsync(oldRecord);
        });
    }

}
