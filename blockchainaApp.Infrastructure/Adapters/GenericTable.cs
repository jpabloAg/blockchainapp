using blockchainaApp.Domain.Ports;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockchainaApp.Infrastructure.Adapters
{
    public class GenericTable<T> : IGenericTable<T> where T : TableEntity, new()
    {
        private readonly CloudTable _table;
        public GenericTable(IConfiguration configuration)
        {
            var storageAccountConnection = configuration.GetConnectionString("StorageAccountConnection");
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageAccountConnection);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
            _table = tableClient.GetTableReference(typeof(T).Name.Replace("TableDto", ""));
            _table.CreateIfNotExists();
        }
        public async Task<T> AddAsync(T entity)
        {
            TableOperation insertOrMerge = TableOperation.Insert(entity);
            TableResult result = await _table.ExecuteAsync(insertOrMerge);
            T insertedEntity = result.Result as T;
            return insertedEntity;
        }

        public IEnumerable<T> GetSync()
        {
            IEnumerable<T> entities = _table.CreateQuery<T>();
            return entities;
        }

        public async Task DeleteAsync(T entity)
        {
            TableOperation delete = TableOperation.Delete(entity);
            await _table.ExecuteAsync(delete);
        }
    }
}
