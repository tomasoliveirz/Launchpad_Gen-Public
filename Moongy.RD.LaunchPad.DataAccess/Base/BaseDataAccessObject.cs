using Microsoft.EntityFrameworkCore;
using Moongy.RD.Launchpad.Data.Base;
using Moongy.RD.Launchpad.Data.Contexts;
using Moongy.RD.LaunchPad.DataAccess.Base.Interfaces;

namespace Moongy.RD.LaunchPad.DataAccess.Base;

public class BaseDataAccessObject<T>(LaunchpadContext context) : IBaseDataAccessObject<T> where T : Entity
{

    public async Task<Guid> CreateAsync(T contractType)
    {
        var result = await context.AddAsync(contractType);
        await context.SaveChangesAsync();
        return result.Entity.Uuid ?? Guid.Empty;
    }

    public async Task DeleteAsync(T contractType)
    {
        context.Remove(contractType);
        await context.SaveChangesAsync();
    }

    public async Task<T?> GetAsync(Guid contractId)
    {
        var result = await context.Set<T>().Where(x => x.Uuid == contractId).SingleOrDefaultAsync();
        return result;
    }

    public async Task<IEnumerable<T>> ListAsync()
    {
        var result = await context.Set<T>().ToListAsync();
        return result;
    }

    public async Task<(int, IEnumerable<T>)> ListAsync(int offset, int limit)
    {
        var result = await context.Set<T>().Skip(offset).Take(limit).ToListAsync();
        var total = await context.Set<T>().CountAsync();
        return (total, result);
    }

    public async Task UpdateAsync(T contractType)
    {
        context.Update(contractType);
        await context.SaveChangesAsync();
    }
}
