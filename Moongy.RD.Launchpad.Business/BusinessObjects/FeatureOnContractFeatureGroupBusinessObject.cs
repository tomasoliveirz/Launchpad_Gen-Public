using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Business.Exceptions;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Base.Interfaces;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;

namespace Moongy.RD.Launchpad.Business.BusinessObjects
{
    public class FeatureOnContractFeatureGroupBusinessObject(IFeatureOnContractFeatureGroupDataAccessObject dao, IGenericDataAccessObject genericDao) : EntityBusinessObject<FeatureOnContractFeatureGroup>(dao), IFeatureOnContractFeatureGroupBusinessObject
    {
        public async Task<OperationResult<Guid>> CreateAsync(FeatureOnContractFeatureGroup feature, Guid featureUuid, Guid featureGroupUuid)
        {
            return await ExecuteOperation(async () =>
            {
                var contractFeature = await genericDao.GetAsync<ContractFeature>(featureUuid) ?? throw new NotFoundException("Contract Feature", featureUuid.ToString());
                var featureGroup = await genericDao.GetAsync<ContractFeatureGroup>(featureGroupUuid) ?? throw new NotFoundException("Contract Feature Group", featureGroupUuid.ToString());
                feature.ContractFeatureGroupId = featureGroup.Id;
                feature.ContractFeatureId = contractFeature.Id;

                var result = await dao.CreateAsync(feature);
                return result;
            });
        }

        public async Task<OperationResult> UpdateAsync(Guid uuid , FeatureOnContractFeatureGroup feature, Guid? featureUuid, Guid? featureGroupUuid)
        {
            return await ExecuteOperation(async () =>
            {
                var oldRecord = await dao.GetAsync(uuid) ?? throw new NotFoundException("Feature in Contract Feature Group", uuid.ToString());
                if (featureUuid != null)
                {
                    var contractFeature = await genericDao.GetAsync<ContractFeature>(featureUuid.Value) ?? throw new NotFoundException("Contract Feature", featureUuid.Value.ToString());
                    oldRecord.ContractFeatureId = contractFeature.Id;
                }

                if (featureGroupUuid != null)
                {
                    var featureGroup = await genericDao.GetAsync<ContractFeatureGroup>(featureGroupUuid.Value) ?? throw new NotFoundException("Contract Feature Group", featureGroupUuid.Value.ToString());
                    oldRecord.ContractFeatureGroupId = featureGroup.Id;
                }
                await dao.UpdateAsync(oldRecord);
            });
        }
    }
}
