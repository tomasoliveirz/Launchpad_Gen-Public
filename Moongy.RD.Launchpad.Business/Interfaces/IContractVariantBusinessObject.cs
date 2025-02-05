using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Data.Entities;

namespace Moongy.RD.Launchpad.Business.Interfaces;

public interface IContractVariantBusinessObject : IEntityBusinessObject<ContractVariant>
{
    public Task<OperationResult<Guid>> CreateAsync(ContractVariant contractVariant, Guid contractTypeUuid);
    public Task<OperationResult> UpdateAsync(Guid uuid, ContractVariant contractVariant, Guid? contractTypeUuid);
}
