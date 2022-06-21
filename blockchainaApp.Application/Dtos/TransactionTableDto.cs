using blockchainaApp.Domain.Enumerations;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockchainaApp.Application.Dtos
{
    public class TransactionTableDto : TableEntity
    {
        public TransactionTableDto(){}
        public TransactionTableDto(TransactionsEnum transactionType)
        {
            PartitionKey = transactionType.ToString();
            RowKey = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string WalletId { get; set; }
        public string WalletReceiver { get; set; }
        public string TransactionMessage { get; set; }
        public string PreviousState { get; set; }
        public string NewState { get; set; }
    }
}
