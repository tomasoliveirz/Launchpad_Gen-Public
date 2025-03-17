using Microsoft.EntityFrameworkCore;
using Moongy.RD.Launchpad.Data.Contexts;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Base;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;

namespace Moongy.RD.LaunchPad.DataAccess.DataAccessObjects
{
    public class ContractTypeDataAccessObject(LaunchpadContext context) : BaseDataAccessObject<ContractType>(context), IContractTypeDataAccessObject
    {
        public async Task<IEnumerable<(ContractType, int)>> GetContractTypesWithVariantCount()
        {
            var query = from types in context.ContractTypes
                        join variants in context.ContractsVariants on types.Id equals variants.ContractTypeId
                        select new { Type = types, Variant = variants };

            var subQuery = from queryResult in query 
                           group queryResult by queryResult.Type into g
                           select new {Type = g.Key, Variants = g};

            var subQueryResult = await subQuery.ToListAsync();
            var result = new List<(ContractType, int)>();

            foreach (var item in subQueryResult) 
            {
                result.Add((item.Type, item.Variants.Count()));    
            }
            return result;
        }

        public async Task<IEnumerable<FeatureInContractType>> GetFeaturesInContractType(Guid contractTypeUuid)
        {
            return await context.FeatureInContractTypes.Include(x => x.ContractFeature).Include(x => x.ContractType).Where(x => x.ContractType.Uuid == contractTypeUuid).ToListAsync();
        }
    }
}
