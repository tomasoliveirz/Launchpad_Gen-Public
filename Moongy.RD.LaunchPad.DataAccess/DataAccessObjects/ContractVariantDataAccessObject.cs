using Microsoft.EntityFrameworkCore;
using Moongy.RD.Launchpad.Data.Contexts;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Base;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;


namespace Moongy.RD.LaunchPad.DataAccess.DataAccessObjects;

public class ContractVariantDataAccessObject(LaunchpadContext context) : BaseDataAccessObject<ContractVariant>(context), IContractVariantDataAccessObject
{
    public async Task<IEnumerable<(ContractVariant, string?)>> GetContractVariantAndTypeName()
    {


        var query = from variants in context.ContractsVariants
                    join types in context.ContractTypes on variants.ContractTypeId equals types.Id
                    select new { Type = types, Variant = variants };


        var queryResult = await query.ToListAsync();
        var result = new List<(ContractVariant, string?)>();

        foreach (var item in queryResult)
        {
            result.Add((item.Variant, item.Type?.Name));
        }
        return result;
    }
}
