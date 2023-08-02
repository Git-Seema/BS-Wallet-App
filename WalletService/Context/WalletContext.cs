using Microsoft.EntityFrameworkCore;
using WalletService.Models;

namespace WalletService.Context
{
    public class WalletContext : DbContext
    {
        public WalletContext(DbContextOptions<WalletContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }

}
