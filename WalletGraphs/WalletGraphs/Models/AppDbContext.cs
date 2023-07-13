using Microsoft.EntityFrameworkCore;

namespace WalletGraphs.Models
{
    public class AppDbContext:DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Expenditure> Expenditures { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
                
        }

        
    }
}
