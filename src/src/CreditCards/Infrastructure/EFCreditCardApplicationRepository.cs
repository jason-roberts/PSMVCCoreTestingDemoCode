using System.Threading.Tasks;
using CreditCards.Core.Interfaces;
using CreditCards.Core.Model;

namespace CreditCards.Infrastructure
{
    public class EntityFrameworkCreditCardApplicationRepository : ICreditCardApplicationRepository
    {
        private readonly AppDbContext _dbContext;

        public EntityFrameworkCreditCardApplicationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddAsync(CreditCardApplication application)
        {
            _dbContext.CreditCardApplications.Add(application);

            return _dbContext.SaveChangesAsync();
        }
    }
}