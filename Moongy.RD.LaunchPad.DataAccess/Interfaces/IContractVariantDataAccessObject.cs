using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Base.Interfaces;
namespace Moongy.RD.LaunchPad.DataAccess.Interfaces;

public interface IContractVariantDataAccessObject : IBaseDataAccessObject<ContractVariant>
{
    Task<IEnumerable<ContractVariant>> GetContractVariantsWithType();

    Task<ContractVariant> GetContractVariantWithType(Guid contractVariantUuid);

}
