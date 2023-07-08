using Microsoft.EntityFrameworkCore;

namespace WalletGraphs.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
                
        }

        DbSet<User> Users { get; set; }
        DbSet<Expenditure> Expenditures { get; set; }
    }
}
