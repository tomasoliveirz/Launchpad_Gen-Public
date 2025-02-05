using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moongy.RD.Launchpad.Data.Base;
using Moongy.RD.Launchpad.Data.Contexts;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Base.Interfaces;

namespace Moongy.RD.LaunchPad.DataAccess.Base
{
    public class GenericDataAccessObject(LaunchpadContext context) : IGenericDataAccessObject
    {
        public async Task<Guid> CreateAsync<T>(T e) where T : Entity
        {
            var result = await context.AddAsync(e);
            await context.SaveChangesAsync();
            return result.Entity.Uuid ?? Guid.Empty;
        }

        public async Task DeleteAsync<T>(T e) where T : Entity
        {
            context.Remove(e);
            await context.SaveChangesAsync();
        }

        public async Task<T?> GetAsync<T>(Guid id) where T : Entity
        {
            var result = await context.Set<T>().Where(x => x.Uuid == id).SingleOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<T>> ListAsync<T>() where T : Entity
        {
            var result = await context.Set<T>().ToListAsync();
            return result;
        }

        public async Task<(int, IEnumerable<T>)> ListAsync<T>(int offset, int limit) where T : Entity
        {
            var result = await context.Set<T>().Skip(offset).Take(limit).ToListAsync();
            var total = await context.Set<T>().CountAsync();
            return (total, result);
        }

        public async Task UpdateAsync<T>(T e) where T : Entity
        {
            context.Update(e);
            await context.SaveChangesAsync();
        }
    }
}
