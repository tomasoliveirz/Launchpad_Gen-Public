using Microsoft.EntityFrameworkCore;
using Moongy.RD.Launchpad.Data.Contexts;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Base;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;

namespace Moongy.RD.LaunchPad.DataAccess.DataAccessObjects;

public class ContractVariantDataAccessObject(LaunchpadContext context) : BaseDataAccessObject<ContractVariant>(context), IContractVariantDataAccessObject
{
    public async Task<IEnumerable<ContractVariant>> GetContractVariantsWithType()
    {
        return await context.ContractsVariants.Include(x => x.ContractType).ToListAsync();
    }

    public async Task<ContractVariant> GetContractVariantWithType(Guid contractVariantUuid)
    {
        return await context.ContractsVariants.Include(x => x.ContractType).FirstOrDefaultAsync(x => x.Uuid == contractVariantUuid);
    }

    public async Task<IEnumerable<CharacteristicInContractVariant>> GetCharacteristicsInContractVariant(Guid contractVariantUuid)
    {
        return await context.CharacteristicInContractVariants.Include(x => x.ContractCharacteristic).Include(x => x.ContractVariant).Where(x => x.ContractVariant.Uuid == contractVariantUuid).ToListAsync();
    }
}
