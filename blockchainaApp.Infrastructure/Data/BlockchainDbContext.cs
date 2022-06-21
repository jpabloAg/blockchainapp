using blockchainaApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace blockchainaApp.Infrastructure.Data
{
    public class BlockchainDbContext : DbContext
    {
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Block> Block { get; set; }
        public BlockchainDbContext(DbContextOptions<BlockchainDbContext> options) : base(options) {}
    }
}
