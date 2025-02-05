using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Data.Entities;

namespace Moongy.RD.Launchpad.Business.Interfaces
{
    public interface IFeatureOnContractFeatureGroupBusinessObject : IEntityBusinessObject<FeatureOnContractFeatureGroup>
    {
        public Task<OperationResult<Guid>> CreateAsync(FeatureOnContractFeatureGroup feature, Guid featureUuid, Guid featureGroupUuid);
        public Task<OperationResult> UpdateAsync(Guid uuid, FeatureOnContractFeatureGroup feature, Guid featureUuid, Guid featureGroupUuid);
    }
}
