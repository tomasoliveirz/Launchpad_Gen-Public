using Moongy.RD.Launchpad.Business.Base;
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
                var contractFeature = await genericDao.GetAsync<ContractFeature>(featureUuid);
                if (contractFeature == null) throw new Exception("Contract Feature not found");

                var featureGroup = await genericDao.GetAsync<ContractFeatureGroup>(featureGroupUuid);
                if (featureGroup == null) throw new Exception("Contract Feature Group not found");

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
                var oldRecord = await dao.GetAsync(uuid);
                if (oldRecord == null) throw new Exception("Contract Feature on group not found");

                if(featureUuid != null)
                {
                    var contractFeature = await genericDao.GetAsync<ContractFeature>(featureUuid.Value);
                    if (contractFeature == null) throw new Exception("Contract Feature not found");
                    oldRecord.ContractFeatureId = contractFeature.Id;
                }

                if (featureGroupUuid != null)
                {
                    var featureGroup = await genericDao.GetAsync<ContractFeatureGroup>(featureGroupUuid.Value);
                    if (featureGroup == null) throw new Exception("Contract Feature Group not found");
                    oldRecord.ContractFeatureGroupId = featureGroup.Id;
                }
                await dao.UpdateAsync(oldRecord);
            });
        }
    }
}
