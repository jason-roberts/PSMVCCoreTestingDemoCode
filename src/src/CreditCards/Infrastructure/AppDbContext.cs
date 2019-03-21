using CreditCards.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace CreditCards.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) :
            base(dbContextOptions)
        {
        }

        public DbSet<CreditCardApplication> CreditCardApplications { get; set; }
    }
}