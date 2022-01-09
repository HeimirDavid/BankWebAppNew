using BankWebApp.Infrastructure.Paging;
using BankWebApp.Models;

namespace BankWebApp.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly BankContext _context;

        public CustomerService(BankContext bankContext)
        {
            _context = bankContext;
        }

        public int NumberOfAllCustomers()
        {
            int NumOfCustomers = _context.Customers.Count();
            return NumOfCustomers;
        }

        public PagedResult<Customer> GetAll(int page)
        {
            var query = _context.Customers;
            return query.GetPaged(page, 5);
        }
    }
}
