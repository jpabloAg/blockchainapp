using blockchainaApp.Application.Wallet.Commands.Request;
using blockchainaApp.Domain.Entities;
using blockchainaApp.Domain.Ports;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace blockchainaApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IGenericRepository<Wallet> _repository;
        public WalletController(IMediator mediator, IGenericRepository<Wallet> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wallet>>> GetWallets()
        {
            return Ok(await _repository.GetAsync());
        }

        [HttpPost("createWallet")]
        public async Task<ActionResult<Wallet>> CreateWallet(CreateWalletRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
