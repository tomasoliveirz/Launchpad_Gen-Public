using Moongy.RD.Launchpad.Business.Base;
using Moongy.RD.Launchpad.Data.Entities;

namespace Moongy.RD.Launchpad.Business.Interfaces
{
    public interface IContractTypeBusinessObject : IEntityBusinessObject<ContractType>
    {
        public Task<OperationResult<IEnumerable<FeatureInContractType>>> GetFeaturesInContractType(Guid contractTypeUuid);

    }
}
