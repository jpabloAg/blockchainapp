using blockchainaApp.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace blockchainaApp.Infrastructure.Adapters
{
    public class Encrypt : IEncrypt
    {
        public string GetSHA256(string input)
        {
            var sha256 = SHA256.Create();
            var sb = new StringBuilder();
            byte[] stream = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}
