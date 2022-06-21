using MediatR;

namespace blockchainaApp.Application.Wallet.Commands.Request
{
    public class CreateWalletRequest : IRequest<Domain.Entities.Wallet>
    {
        public string Owner { get; set; }
        public int Amount { get; set; }
    }
}
