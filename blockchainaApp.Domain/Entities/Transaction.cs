using System;

namespace blockchainaApp.Domain.Entities
{
    public class Transaction : EntityBase
    {
        public string WalletId { get; set; }
        public string BlockId { get; set; }
        public string WalletReceiver { get; set; }
        public string TransactionMessage { get; set; }
        public string PreviousState { get; set; }
        public string NewState { get; set; }
        

        // Propiedades de navegación
        public Wallet Wallet { get; set; }
        public Block Block { get; set; }
    }
}