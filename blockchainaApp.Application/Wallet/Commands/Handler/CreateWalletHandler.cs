using blockchainaApp.Application.Wallet.Commands.Request;
using blockchainaApp.Domain.Ports;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace blockchainaApp.Application.Wallet.Commands.Handler
{
    public class CreateWalletHandler : IRequestHandler<CreateWalletRequest, Domain.Entities.Wallet>
    {
        private readonly IGenericRepository<Domain.Entities.Wallet> _repository;
        public CreateWalletHandler(IGenericRepository<Domain.Entities.Wallet> repository)
        {
            _repository = repository;
        }
        public async Task<Domain.Entities.Wallet> Handle(CreateWalletRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException("El request es requerdido para procesar la petición");

            return await _repository.AddAsync(new Domain.Entities.Wallet 
            {
                Id = Guid.NewGuid().ToString(),
                Owner = request.Owner,
                Amount = request.Amount
            });
        }
    }
}
