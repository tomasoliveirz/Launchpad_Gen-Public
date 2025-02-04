using Moongy.RD.Launchpad.Data.Entities;
namespace Moongy.RD.LaunchPad.DataAccess.Interfaces;

public interface IContractTypeDataAccessObject
{
    Task<Guid> CreateAsync(ContractType contractType);
    Task<ContractType?> GetAsync(Guid contractId);
    Task<IEnumerable<ContractType>> ListAsync();
    Task UpdateAsync (ContractType contractType);
    Task DeleteAsync (ContractType contractType);

    Task<(int, IEnumerable<ContractType>)> ListAsync(int offset, int limit);
}
