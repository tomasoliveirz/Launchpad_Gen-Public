using Microsoft.EntityFrameworkCore;
using Moongy.RD.Launchpad.Data.Contexts;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Base;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;

namespace Moongy.RD.LaunchPad.DataAccess.DataAccessObjects;

public class ContractVariantDataAccessObject(LaunchpadContext context) : BaseDataAccessObject<ContractVariant>(context), IContractVariantDataAccessObject
{
    public async Task<IEnumerable<ContractVariant>> GetContractVariantWithType()
    {


        return await context.ContractsVariants.Include(x => x.ContractType).ToListAsync();
    }
}
