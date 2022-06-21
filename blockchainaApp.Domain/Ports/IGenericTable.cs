using Microsoft.Azure.Cosmos.Table;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace blockchainaApp.Domain.Ports
{
    public interface IGenericTable<T> where T : TableEntity
    {
        Task<T> AddAsync(T entity);
        IEnumerable<T> GetSync();
        Task DeleteAsync(T entity);
    }
}
