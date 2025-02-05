using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Business.Exceptions;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;

namespace Moongy.RD.Launchpad.Business.BusinessObjects;

public class ContractFeatureBusinessObject(IContractFeatureDataAccessObject dao) : EntityBusinessObject<ContractFeature>(dao), IContractFeatureBusinessObject
{
    public override async Task<OperationResult<Guid>> CreateAsync(ContractFeature contractFeature)
    {
        return await ExecuteOperation(async () =>
        {
            if (string.IsNullOrEmpty(contractFeature.Name)) throw new InvalidModelException("name is missing");
            if (string.IsNullOrEmpty(contractFeature.DataType)) throw new InvalidModelException("dataType is missing");
            var result = await dao.CreateAsync(contractFeature);
            return result;
        });
    }

    public override async Task<OperationResult> UpdateAsync(Guid uuid, ContractFeature contractFeature)
    {
        return await ExecuteOperation(async () =>
        {
            if (string.IsNullOrEmpty(contractFeature.Name)) throw new InvalidModelException("name is missing");
            var oldRecord = await dao.GetAsync(uuid) ?? throw new NotFoundException("Contract Feature", uuid.ToString());
            oldRecord.Name = contractFeature.Name;
            oldRecord.Description = contractFeature.Description;
            oldRecord.DataType = contractFeature.DataType;
            await dao.UpdateAsync(oldRecord);
        });
    }
}
