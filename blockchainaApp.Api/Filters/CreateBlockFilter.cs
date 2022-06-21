using blockchainaApp.Application.Dtos;
using blockchainaApp.Domain.Ports;
using blockchainaApp.Application.Blockchain.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace blockchainaApp.Api.Filters
{
    public class CreateBlockFilter : IAsyncActionFilter
    {
        private readonly IGenericTable<TransactionTableDto> _table;
        private readonly IMediator _mediator;
        private readonly ILogger<CreateBlockFilter> _logger;
        public CreateBlockFilter(IGenericTable<TransactionTableDto> table, IMediator mediator, ILogger<CreateBlockFilter> logger)
        {
            _table = table;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();
            var transanctions = _table.GetSync().ToList();
            if(transanctions.Count() >= 4)
            {
                var transanctionsForBlock = transanctions.OrderBy(x => x.Timestamp).Take(4);
                foreach (var transaction in transanctionsForBlock) _logger.LogInformation(transaction.TransactionMessage);

                var isBlockAdded = await _mediator.Send(new GenerateBlockRequest() { Transactions = transanctionsForBlock });

                if (isBlockAdded)
                {
                    foreach(var transaction in transanctionsForBlock) await _table.DeleteAsync(transaction);
                }
            }
            else
            {
                _logger.LogInformation($"Apenas hay {transanctions.Count()} transacciones, por lo que aun no se creará un nuevo bloque");
            }
        }
    }
}
