using System.Collections.Generic;

namespace blockchainaApp.Domain.Entities
{
    public class Wallet : EntityBase
    {
        public string Owner { get; set; }
        public int Amount { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
