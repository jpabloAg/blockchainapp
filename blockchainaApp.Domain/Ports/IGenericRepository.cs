using blockchainaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace blockchainaApp.Domain.Ports
{
    public interface IGenericRepository<T> where T : EntityBase
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetByIdAsync(string id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
    }
}
