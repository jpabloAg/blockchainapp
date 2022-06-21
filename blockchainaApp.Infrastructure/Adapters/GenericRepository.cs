using blockchainaApp.Domain.Entities;
using blockchainaApp.Domain.Ports;
using blockchainaApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace blockchainaApp.Infrastructure.Adapters
{
    public class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
    {
        private readonly BlockchainDbContext _context;
        private DbSet<T> _entity;
        public GenericRepository(BlockchainDbContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _entity.ToListAsync();
        }
        
        public async Task<T> GetByIdAsync(string id)
        {
            return await _entity.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _entity.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _entity.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
