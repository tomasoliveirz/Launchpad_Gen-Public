using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Moongy.RD.Launchpad.Data.Contexts;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Base;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;

namespace Moongy.RD.LaunchPad.DataAccess.DataAccessObjects;

public class CharacteristicInContractVariantDataAccessObject([NotNull] LaunchpadContext context) : BaseDataAccessObject<CharacteristicInContractVariant>(context), ICharacteristicInContractVariantDataAccessObject
{
    public async Task<IEnumerable<CharacteristicInContractVariant>> GetCharacteristicsInContractVariants()
    {
        return await context.CharacteristicInContractVariants.Include(x => x.ContractCharacteristic).Include(x => x.ContractVariant).ToListAsync();
    }

    public async Task<CharacteristicInContractVariant> GetCharacteristicInContractVariant(Guid contractCharacteristicUuid)
    {
        return await context.CharacteristicInContractVariants.Include(x => x.ContractCharacteristic).Include(x => x.ContractVariant).FirstOrDefaultAsync(x => x.Uuid == contractCharacteristicUuid);
    }
}
