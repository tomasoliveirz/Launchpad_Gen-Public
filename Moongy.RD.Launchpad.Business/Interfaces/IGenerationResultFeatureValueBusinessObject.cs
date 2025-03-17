using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Data.Entities;

namespace Moongy.RD.Launchpad.Business.Interfaces
{
    public interface IGenerationResultFeatureValueBusinessObject : IEntityBusinessObject<GenerationResultFeatureValue>
    {
        public Task<OperationResult<Guid>> CreateAsync(GenerationResultFeatureValue value, Guid featureOnGroupFeatureUuid, Guid generationResultUuid);
        public Task<OperationResult> UpdateAsync(Guid uuid, GenerationResultFeatureValue value, Guid? featureOnGroupFeatureUuid, Guid? generationResultUuid);
    }
}
