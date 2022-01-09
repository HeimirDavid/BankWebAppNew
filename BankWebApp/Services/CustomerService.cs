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
    }
}
