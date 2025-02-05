using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Data.Entities;

namespace Moongy.RD.Launchpad.Business.Interfaces;

public interface IContractGenerationResultBusinessObject : IEntityBusinessObject<ContractGenerationResult>
{
    public Task<OperationResult<Guid>> CreateAsync(ContractGenerationResult contractGenerationResult, Guid contractVariantUuid);

    public Task<OperationResult> UpdateAsync(Guid uuid, ContractGenerationResult contractGenerationResult, Guid? contractVariantUuid);
}
