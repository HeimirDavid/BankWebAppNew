using BankWebApp.Infrastructure.Paging;
using BankWebApp.Models;

namespace BankWebApp.Services
{
    public interface ICustomerService
    {
        public int NumberOfAllCustomers();

        public PagedResult<Customer> GetAll(int page);
    }
}
