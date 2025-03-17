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
    public class GenerationResultFeatureValueBusinessObject(IGenerationResultFeatureValueDataAccessObject dao, IGenericDataAccessObject genericDao) : EntityBusinessObject<GenerationResultFeatureValue>(dao, genericDao), IGenerationResultFeatureValueBusinessObject
    {
        public async Task<OperationResult<Guid>> CreateAsync(GenerationResultFeatureValue value, Guid featureInContractTypeUuid, Guid contractGenerationResultUuid)
        {
            return await ExecuteOperation(async () =>
            {
                var featureInContractType = await genericDao.GetAsync<FeatureInContractType>(featureInContractTypeUuid) ?? throw new NotFoundException("Feature in Contract Type", featureInContractTypeUuid.ToString());
                var generationResult = await genericDao.GetAsync<ContractGenerationResult>(contractGenerationResultUuid) ?? throw new NotFoundException("Generation Result", contractGenerationResultUuid.ToString());
                value.FeatureInContractTypeId = featureInContractType.Id;
                value.ContractGenerationResultId = generationResult.Id;

                var result = await dao.CreateAsync(value);
                return result;
            });
        }

        public async Task<OperationResult> UpdateAsync(Guid uuid, GenerationResultFeatureValue feature, Guid? featureInContractTypeUuid, Guid? contractGenerationResultUuid)
        {
            return await ExecuteOperation(async () =>
            {
                var oldRecord = await dao.GetAsync(uuid) ?? throw new NotFoundException("Feature in Contract Type", uuid.ToString());
                if (featureInContractTypeUuid != null)
                {
                    var featureInContractType = await genericDao.GetAsync<FeatureInContractType>(featureInContractTypeUuid.Value) ?? throw new NotFoundException("Contract Feature on Feature Group", featureInContractTypeUuid.Value.ToString());
                    oldRecord.FeatureInContractTypeId = featureInContractType.Id;
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
