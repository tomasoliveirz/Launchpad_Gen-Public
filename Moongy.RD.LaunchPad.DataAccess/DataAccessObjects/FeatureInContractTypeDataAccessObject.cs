using Microsoft.EntityFrameworkCore;
using Moongy.RD.Launchpad.Data.Contexts;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Base;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;

namespace Moongy.RD.LaunchPad.DataAccess.DataAccessObjects;

public class FeatureInContractTypeDataAccessObject(LaunchpadContext context) : BaseDataAccessObject<FeatureInContractType>(context), IFeatureInContractTypeDataAccessObject
{
    public async Task<IEnumerable<FeatureInContractType>> GetFeatureInContractTypes()
    {
        return await context.FeatureInContractTypes.Include(x => x.ContractFeature).Include(x => x.ContractType).ToListAsync();
    }

    public async Task<FeatureInContractType> GetFeatureInContractType(Guid contractFeatureUuid)
    {
        return await context.FeatureInContractTypes.Include(x => x.ContractFeature).Include(x => x.ContractType).FirstOrDefaultAsync(x => x.Uuid == contractFeatureUuid);
    }
}
