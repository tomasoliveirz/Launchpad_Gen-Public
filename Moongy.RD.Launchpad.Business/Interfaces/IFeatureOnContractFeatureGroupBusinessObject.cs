using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Data.Entities;

namespace Moongy.RD.Launchpad.Business.Interfaces
{
    public interface IFeatureOnContractFeatureGroupBusinessObject : IEntityBusinessObject<FeatureOnContractFeatureGroup>
    {
        public Task<OperationResult<Guid>> CreateAsync(FeatureOnContractFeatureGroup feature, Guid featureUuid, Guid featureGroupUuid);
        public Task<OperationResult> UpdateAsync(Guid uuid, FeatureOnContractFeatureGroup feature, Guid? featureUuid, Guid? featureGroupUuid);
    }
}
