using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Data.Entities;


namespace Moongy.RD.Launchpad.Business.Interfaces;

public interface IContractFeatureGroupBusinessObject : IEntityBusinessObject<ContractFeatureGroup>
{
    public Task<OperationResult<Guid>> CreateAsync(ContractFeatureGroup contractVariant, Guid contractGenerationResultUuid);
    public Task<OperationResult> UpdateAsync(Guid uuid, ContractFeatureGroup contractVariant, Guid? contractGenerationResultUuid);
}
