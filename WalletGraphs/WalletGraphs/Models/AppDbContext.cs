using Microsoft.EntityFrameworkCore;

namespace WalletGraphs.Models
{
    public class AppDbContext:DbContext
    {

        DbSet<User> Users { get; set; }
        DbSet<Expenditure> Expenditures { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
                
        }

        
    }
}
