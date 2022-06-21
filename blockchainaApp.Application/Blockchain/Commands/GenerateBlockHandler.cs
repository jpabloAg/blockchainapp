using blockchainaApp.Application.Blockchain.Request;
using blockchainaApp.Application.Dtos;
using blockchainaApp.Domain.Entities;
using blockchainaApp.Domain.Ports;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace blockchainaApp.Application.Blockchain.Commands
{
    internal class GenerateBlockHandler : IRequestHandler<GenerateBlockRequest, bool>
    {
        private readonly IGenericRepository<Block> _repository;
        private readonly IMerkelTreeHashing _mk;
        private readonly IProofOfWork _pf;
        public GenerateBlockHandler(IGenericRepository<Block> repository, IProofOfWork pf, IMerkelTreeHashing mk)
        {
            _repository = repository;
            _pf = pf;
            _mk = mk;
        }
        public async Task<bool> Handle(GenerateBlockRequest request, CancellationToken cancellationToken)
        {
            var blockChain = new Domain.Entities.Blockchain(_repository, _pf);
            await blockChain.InitializeBlockchain();

            var newBlock = new Block()
            {
                previousBlockHash = blockChain.Blocks.Last().Id,
                TimeStamp = DateTime.Now,
                Height = blockChain.Blocks.Count() + 1,
                RootHash = _mk.GetMerkelTreeRootHash(request.Transactions.Select(x => x.Id).ToList()),
                Transactions = GetTransactions(request.Transactions).ToList()
            };

            newBlock.Id = _pf.MakeProofOfWork(newBlock);
            foreach (var transaction in newBlock.Transactions) transaction.BlockId = newBlock.Id;
            var isBlockAdded = await _repository.AddAsync(newBlock);

            return isBlockAdded != null ? true : false;
        }

        private IEnumerable<Domain.Entities.Transaction> GetTransactions(IEnumerable<TransactionTableDto> transactionsTemp)
        {
            var transactions = transactionsTemp.Select(x => new Domain.Entities.Transaction()
            {
                Id = x.Id,
                WalletId = x.WalletId,
                WalletReceiver = x.WalletReceiver,
                TransactionMessage = x.TransactionMessage,
                PreviousState = x.PreviousState,
                NewState = x.NewState
            });
            return transactions;
        } 
    }
}
