using BankWebApp.Infrastructure.Paging;
using BankWebApp.Models;
using BankWebApp.ViewModels;

namespace BankWebApp.Services
{
    public interface ICustomerService
    {
        public int NumberOfAllCustomers();

        public PagedResult<Customer> GetAll(int page, string sortColumn, string sortOrder, string searchWord);

        public CustomerView GetCustomer(int id);

        public Customer GetCustomerWithAccountNo(int accountId);
        public IEnumerable<Disposition> GetAllDispositions();

        public void AddCustomer(NewCustomerViewModel customer);
    }
}
