using blockchainaApp.Application.Dtos;
using blockchainaApp.Domain.Enumerations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockchainaApp.Application.Transaction.Commands.Request
{
    public class GenerateTransactionRequest : IRequest<TransactionTableDto>
    {
        public string WalletId { get; set; }
        public string WalletReceiver { get; set; }
        public int Amount { get; set; }
        public TransactionsEnum TransactionType { get; set; }
    }
}
