using Microsoft.EntityFrameworkCore;
using Moongy.RD.Launchpad.Data.Contexts;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Base.Interfaces;

namespace Moongy.RD.LaunchPad.DataAccess.Base;

public class UniversalDataAccessObject<T>(LaunchpadContext context) : IUniversalDataAccessObject<T>
{
    private readonly LaunchpadContext _context = context;

    public async Task<Guid> CreateAsync(T contractType)
    {
        var result = await _context.AddAsync(contractType);
        await _context.SaveChangesAsync();
        return result.Entity.UUid;
    }

    public async Task DeleteAsync(T contractType)
    {
        _context.Remove(contractType);
        await _context.SaveChangesAsync();
    }

    public async Task<ContractType?> GetAsync(Guid contractId)
    {
        var result = await _context.ContractTypes.Where(x => x.UUid == contractId).SingleOrDefaultAsync();
        return result;
    }

    public async Task<IEnumerable<ContractType>> ListAsync()
    {
        var result = await _context.ContractTypes.ToListAsync();
        return result;
    }

    public async Task<(int, IEnumerable<ContractType>)> ListAsync(int offset, int limit)
    {
        var result = await _context.ContractTypes.Skip(offset).Take(limit).ToListAsync();
        var total = await _context.ContractTypes.CountAsync();
        return (total, result);
    }

    public async Task UpdateAsync(ContractType contractType)
    {
        _context.Update(contractType);
        await _context.SaveChangesAsync();
    }
}
