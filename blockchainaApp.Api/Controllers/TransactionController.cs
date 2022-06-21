using blockchainaApp.Domain.Entities;
using blockchainaApp.Application.Transaction.Commands.Request;
using blockchainaApp.Domain.Ports;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using blockchainaApp.Api.Filters;

namespace blockchainaApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IGenericRepository<Transaction> _repository;
        public TransactionController(IMediator mediator, IGenericRepository<Transaction> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            return Ok(await _repository.GetAsync());
        }

        [ServiceFilter(typeof(CreateBlockFilter))]
        [HttpPost("generateTransaction")]
        public async Task<ActionResult> GenerateTransanction(GenerateTransactionRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
