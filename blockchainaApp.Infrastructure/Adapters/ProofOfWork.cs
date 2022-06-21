using blockchainaApp.Domain.Entities;
using blockchainaApp.Domain.Ports;
using System;
using System.Text.Json;

namespace blockchainaApp.Infrastructure.Adapters
{
    public class ProofOfWork : IProofOfWork
    {
        private readonly IEncrypt _encrypt;
        public ProofOfWork(IEncrypt encrypt)
        {
            _encrypt = encrypt;
        }

        public string MakeProofOfWork(Block block)
        {
            var hashBlock = "";
            var isHashValidate = false;
            var rnd = new Random();
            do
            {
                block.Nonce = rnd.Next();
                var jsonString = JsonSerializer.Serialize(block);
                hashBlock = _encrypt.GetSHA256(jsonString);
                isHashValidate = string.Equals(hashBlock.Substring(0, 3), "000");
            } while (!isHashValidate);

            return hashBlock;
        }
    }
}
