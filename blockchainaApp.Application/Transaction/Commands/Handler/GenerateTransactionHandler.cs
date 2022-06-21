using blockchainaApp.Application.Dtos;
using blockchainaApp.Application.Transaction.Commands.Request;
using blockchainaApp.Domain.Enumerations;
using blockchainaApp.Domain.Ports;
using MediatR;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace blockchainaApp.Application.Transaction.Commands.Handler
{
    public class GenerateTransactionHandler : IRequestHandler<GenerateTransactionRequest, TransactionTableDto>
    {
        private readonly IGenericRepository<Domain.Entities.Wallet> _repository;
        private readonly IGenericTable<TransactionTableDto> _table;
        private readonly IEncrypt _encrypt;
        public GenerateTransactionHandler(IGenericRepository<Domain.Entities.Wallet> repository, IGenericTable<TransactionTableDto> table, IEncrypt encrypt)
        {
            _repository = repository;
            _table = table;
            _encrypt = encrypt;
        }
        public async Task<TransactionTableDto> Handle(GenerateTransactionRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException("El request es requerdido para procesar la petición");

            var walletEmmit = await _repository.GetByIdAsync(request.WalletId);
            var walletRec = await _repository.GetByIdAsync(request.WalletReceiver);

            if ((walletEmmit == null) || (walletRec == null)) throw new Exception("No existe alguna de las wallets especificadas");

            var transaction = request.TransactionType switch
            {
                TransactionsEnum.Depositar => await Depositar(walletEmmit, request.Amount),
                TransactionsEnum.Retirar => await Retirar(walletEmmit, request.Amount),
                TransactionsEnum.Consignar => await Consignar(walletEmmit, walletRec, request.Amount),
                _ => throw new ArgumentNullException(nameof(request.TransactionType), "Tipo de Acción invalida")
            };
            transaction.Id = _encrypt.GetSHA256(JsonSerializer.Serialize(transaction));
            await _table.AddAsync(transaction);

            return transaction;
        }

        private async Task<TransactionTableDto> Depositar(Domain.Entities.Wallet wallet, int amount)
        {
            var transaction = new TransactionTableDto(TransactionsEnum.Depositar)
            {
                WalletId = wallet.Id,
                WalletReceiver = wallet.Id,
                TransactionMessage = $"{wallet.Owner} depósito ${amount} a su wallet",
                PreviousState = $"{wallet.Owner} tenía ${wallet.Amount} en su wallet",
                NewState = $"{wallet.Owner} ahora posee ${wallet.Amount + amount} en su wallet"
            };

            wallet.Amount += amount;
            await _repository.UpdateAsync(wallet);
            return transaction;
        }

        private async Task<TransactionTableDto> Retirar(Domain.Entities.Wallet wallet, int amount)
        {
            if (wallet.Amount < amount) throw new Exception($"La wallet no posee la cantidad suficiente para el retiro, solo cuanta con ${wallet.Amount}");

            var transaction = new TransactionTableDto(TransactionsEnum.Retirar)
            {
                WalletId = wallet.Id,
                WalletReceiver = wallet.Id,
                TransactionMessage = $"{wallet.Owner} retiro ${amount} a su wallet",
                PreviousState = $"{wallet.Owner} tenía ${wallet.Amount} en su wallet",
                NewState = $"{wallet.Owner} ahora posee ${wallet.Amount - amount} en su wallet"
            };

            wallet.Amount -= amount;
            await _repository.UpdateAsync(wallet);
            return transaction;
        }

        private async Task<TransactionTableDto> Consignar(Domain.Entities.Wallet walletEmmit, Domain.Entities.Wallet walletRec, int amount)
        {
            if (walletEmmit.Amount < amount) throw new Exception($"La wallet no posee la cantidad suficiente para realizar la consigancíón, solo cuanta con ${walletEmmit.Amount}");
            var transaction = new TransactionTableDto(TransactionsEnum.Consignar)
            {
                WalletId = walletEmmit.Id,
                WalletReceiver = walletRec.Id,
                TransactionMessage = $"{walletEmmit.Owner} consigno ${amount} a la wallet de {walletRec.Owner}",
                PreviousState = $"{walletEmmit.Owner} tenía ${walletEmmit.Amount} en su wallet, mientras que {walletRec.Owner} tenía ${walletRec.Amount} en su wallet",
                NewState = $"{walletEmmit.Owner} ahora posee ${walletEmmit.Amount - amount} en su wallet, mientras que {walletRec.Owner} ahora posee ${walletRec.Amount + amount} en su wallet"
            };
            walletEmmit.Amount -= amount;
            walletRec.Amount += amount;
            await _repository.UpdateAsync(walletEmmit);
            await _repository.UpdateAsync(walletRec);
            return transaction;
        }
    }
}
