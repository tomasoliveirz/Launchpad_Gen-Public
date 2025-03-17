using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.Launchpad.Business.Interfaces;

public interface IFeatureInContractTypeBusinessObject : IEntityBusinessObject<FeatureInContractType>
{
    public Task<OperationResult<Guid>> CreateAsync(FeatureInContractType featureInContractType, Guid contractFeatureUuid, Guid contractTypeUuid);

    public Task<OperationResult> UpdateAsync(Guid uuid, FeatureInContractType featureInContractType, Guid? contractFeatureUuid, Guid? contractTypeUuid);

    public Task<OperationResult<IEnumerable<FeatureInContractType>>> GetFeatureInContractTypes();

    public Task<OperationResult<FeatureInContractType>> GetFeatureInContractType(Guid contractFeatureUuid);
}
