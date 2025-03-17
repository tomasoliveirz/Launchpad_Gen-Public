using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Business.Exceptions;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Base.Interfaces;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.Launchpad.Business.BusinessObjects;

public class FeatureInContractTypeBusinessObject(IFeatureInContractTypeDataAccessObject dao, IGenericDataAccessObject genericDao) : EntityBusinessObject<FeatureInContractType>(dao, genericDao), IFeatureInContractTypeBusinessObject
{
    public async Task<OperationResult<Guid>> CreateAsync(FeatureInContractType featureInContractType, Guid contractFeatureUuid, Guid contractTypeUuid)
    {
        return await ExecuteOperation(async () =>
        {
            featureInContractType = await FindAndAttach(featureInContractType, contractFeatureUuid, x => x.ContractFeature, x => x.ContractFeatureId);
            featureInContractType = await FindAndAttach(featureInContractType, contractTypeUuid, x => x.ContractType, x => x.ContractTypeId);
            featureInContractType.Uuid = null;
            var result = await dao.CreateAsync(featureInContractType);
            return result;
        });
    }

    public async Task<OperationResult> UpdateAsync(Guid uuid, FeatureInContractType featureInContractType, Guid? contractFeatureUuid, Guid? contractTypeUuid)
    {
        return await ExecuteOperation(async () =>
        {
            var oldRecord = await dao.GetAsync(uuid) ?? throw new NotFoundException("Feature In Contract Type", uuid.ToString());
            if (contractFeatureUuid != null)
            {
                var contractFeature = await genericDao.GetAsync<ContractFeature>(contractFeatureUuid.Value) ?? throw new NotFoundException("Contract Feature", uuid.ToString());
                oldRecord.ContractFeatureId = contractFeature.Id;
            }

            if (contractTypeUuid != null)
            {
                var contractType = await genericDao.GetAsync<ContractType>(contractTypeUuid.Value) ?? throw new NotFoundException("Contract Type", uuid.ToString());
                oldRecord.ContractTypeId = contractType.Id;
            }
            await dao.UpdateAsync(oldRecord);
        });
    }

    public async Task<OperationResult<IEnumerable<FeatureInContractType>>> GetFeatureInContractTypes()
    {
        return await ExecuteOperation(async () =>
        {
            var records = await dao.GetFeatureInContractTypes();
            return records;
        });
    }

    public async Task<OperationResult<FeatureInContractType>> GetFeatureInContractType(Guid contractFeatureUuid)
    {
        return await ExecuteOperation(async () =>
        {
            var records = await dao.GetFeatureInContractType(contractFeatureUuid);
            return records;
        });
    }
}
