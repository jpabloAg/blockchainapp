using blockchainaApp.Domain.Ports;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace blockchainaApp.Domain.Entities
{
    public class Block : EntityBase
    {
        public string previousBlockHash { get; set; }
        public string RootHash { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Height { get; set; }
        public int Nonce { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
