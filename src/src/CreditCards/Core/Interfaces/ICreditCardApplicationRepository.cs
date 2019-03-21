using System.Threading.Tasks;
using CreditCards.Core.Model;

namespace CreditCards.Core.Interfaces
{
    public interface ICreditCardApplicationRepository
    {
        Task AddAsync(CreditCardApplication application);
    }
}
