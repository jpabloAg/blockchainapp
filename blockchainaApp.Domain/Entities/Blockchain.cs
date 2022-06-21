using blockchainaApp.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blockchainaApp.Domain.Entities
{
    public class Blockchain
    {
        public IEnumerable<Block> Blocks { get; set; }
        private readonly IGenericRepository<Block> _repository;
        private readonly IProofOfWork _pf;
        public Blockchain(IGenericRepository<Block> repository, IProofOfWork pf)
        {
            _repository = repository;
            _pf = pf;
        }

        public async Task InitializeBlockchain()
        {
            Blocks = await _repository.GetAsync();
            if(Blocks.Count() == 0)
            {
                var blockGenesis = new Block()
                {
                    TimeStamp = DateTime.Now,
                    Height = 0
                    // crear transaccion para la wallet de minado (debe ser una ya definida, creo).
                };

                blockGenesis.Id = _pf.MakeProofOfWork(blockGenesis);
                Blocks.Append(blockGenesis);
                await _repository.AddAsync(blockGenesis);
            }
        }
    }
}
