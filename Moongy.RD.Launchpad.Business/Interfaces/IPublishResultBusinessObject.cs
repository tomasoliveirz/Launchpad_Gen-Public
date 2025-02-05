using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Data.Entities;

namespace Moongy.RD.Launchpad.Business.Interfaces;

public interface IPublishResultBusinessObject : IEntityBusinessObject<PublishResult>
{
    public Task<OperationResult<Guid>> CreateAsync(PublishResult publicResult, Guid contractGenerationResultUuid, Guid blockchainNetworkUuid);

    public Task<OperationResult> UpdateAsync(Guid uuid, PublishResult publicResult, Guid? contractGenerationResultUuid, Guid? blockchainNetworkUuid);
}
