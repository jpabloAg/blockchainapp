using blockchainaApp.Application.Dtos;
using MediatR;
using System.Collections.Generic;

namespace blockchainaApp.Application.Blockchain.Request
{
    public class GenerateBlockRequest : IRequest<bool>
    {
        public IEnumerable<TransactionTableDto> Transactions { get; set; }
    }
}
