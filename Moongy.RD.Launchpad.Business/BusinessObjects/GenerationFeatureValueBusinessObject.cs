using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Business.Exceptions;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Base.Interfaces;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;

namespace Moongy.RD.Launchpad.Business.BusinessObjects
{
 

    public class GenerationFeatureValueBusinessObject(IGenerationFeatureValueDataAccessObject dao, IGenericDataAccessObject genericDao) : EntityBusinessObject<GenerationFeatureValue>(dao, genericDao), IGenerationFeatureValueBusinessObject
    {
        public async Task<OperationResult<Guid>> CreateAsync(GenerationFeatureValue value, Guid featureOnContractFeatureGroupUuid, Guid contractGenerationResultUuid)
        {
            return await ExecuteOperation(async () =>
            {
                var featureOnGroupFeature = await genericDao.GetAsync<FeatureOnContractFeatureGroup>(featureOnContractFeatureGroupUuid) ?? throw new NotFoundException("Contract Feature on Feature Group", featureOnContractFeatureGroupUuid.ToString());
                var generationResult = await genericDao.GetAsync<ContractGenerationResult>(contractGenerationResultUuid) ?? throw new NotFoundException("Generation Result", contractGenerationResultUuid.ToString());
                value.FeatureOnContractFeatureGroupId = featureOnGroupFeature.Id;
                value.ContractGenerationResultId = generationResult.Id;

                var result = await dao.CreateAsync(value);
                return result;
            });
        }

        public async Task<OperationResult> UpdateAsync(Guid uuid, GenerationFeatureValue feature, Guid? featureOnContractFeatureGroupUuid, Guid? contractGenerationResultUuid)
        {
            return await ExecuteOperation(async () =>
            {
                var oldRecord = await dao.GetAsync(uuid) ?? throw new NotFoundException("Feature in Contract Feature Group", uuid.ToString());
                if (featureOnContractFeatureGroupUuid != null)
                {
                    var featureOnGroupFeature = await genericDao.GetAsync<FeatureOnContractFeatureGroup>(featureOnContractFeatureGroupUuid.Value) ?? throw new NotFoundException("Contract Feature on Feature Group", featureOnContractFeatureGroupUuid.Value.ToString());
                    oldRecord.FeatureOnContractFeatureGroupId = featureOnGroupFeature.Id;
                }

                if (contractGenerationResultUuid != null)
                {
                    var generationResult = await genericDao.GetAsync<ContractGenerationResult>(contractGenerationResultUuid.Value) ?? throw new NotFoundException("Generation Result", contractGenerationResultUuid.Value.ToString());
                    oldRecord.ContractGenerationResultId = generationResult.Id;
                }
                await dao.UpdateAsync(oldRecord);
            });
        }
    }
}
