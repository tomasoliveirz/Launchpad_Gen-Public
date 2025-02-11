using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Data.Entities;

namespace Moongy.RD.Launchpad.Business.Interfaces
{
    public interface IGenerationFeatureValueBusinessObject : IEntityBusinessObject<GenerationFeatureValue>
    {
        public Task<OperationResult<Guid>> CreateAsync(GenerationFeatureValue value, Guid featureOnGroupFeatureUuid, Guid generationResultUuid);
        public Task<OperationResult> UpdateAsync(Guid uuid, GenerationFeatureValue value, Guid? featureOnGroupFeatureUuid, Guid? generationResultUuid);
    }
}
