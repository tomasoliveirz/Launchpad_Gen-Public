using Microsoft.EntityFrameworkCore;
using Moongy.RD.Launchpad.Data.Base;
using Moongy.RD.Launchpad.Data.Contexts;
using Moongy.RD.LaunchPad.DataAccess.Base.Interfaces;

namespace Moongy.RD.LaunchPad.DataAccess.Base;

public class BaseDataAccessObject<T>(LaunchpadContext context) : IBaseDataAccessObject<T> where T : Entity
{
    private readonly LaunchpadContext _context = context;

    public async Task<Guid> CreateAsync(T contractType)
    {
        var result = await _context.AddAsync(contractType);
        await _context.SaveChangesAsync();
        return result.Entity.Uuid ?? Guid.Empty;
    }

    public async Task DeleteAsync(T contractType)
    {
        _context.Remove(contractType);
        await _context.SaveChangesAsync();
    }

    public async Task<T?> GetAsync(Guid contractId)
    {
        var result = await _context.Set<T>().Where(x => x.Uuid == contractId).SingleOrDefaultAsync();
        return result;
    }

    public async Task<IEnumerable<T>> ListAsync()
    {
        var result = await _context.Set<T>().ToListAsync();
        return result;
    }

    public async Task<(int, IEnumerable<T>)> ListAsync(int offset, int limit)
    {
        var result = await _context.Set<T>().Skip(offset).Take(limit).ToListAsync();
        var total = await _context.Set<T>().CountAsync();
        return (total, result);
    }

    public async Task UpdateAsync(T contractType)
    {
        _context.Update(contractType);
        await _context.SaveChangesAsync();
    }
}
