using BankWebApp.Infrastructure.Paging;
using BankWebApp.Models;
using Microsoft.EntityFrameworkCore;

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

        public PagedResult<Customer> GetAll(int page, string sortColumn, string sortOrder, string searchWord)
        {
            var query = _context.Customers.Where(c => c.City != "asddddddddddddddd");

            if (!string.IsNullOrEmpty(searchWord))
            {
                query = query.Where(c => c.Givenname.Contains(searchWord) || c.City.Contains(searchWord));
            }
           
            if (string.IsNullOrEmpty(sortColumn))
            {
                sortColumn = "CustomerId";
            }
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "asc";
            }

            if (sortColumn == "CustomerId")
            {
                if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.CustomerId);
                else
                    query = query.OrderBy(c => c.CustomerId);
            }

            if (sortColumn == "CustomerName")
            {
                if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.Givenname);
                else
                    query = query.OrderBy(c => c.Givenname);
            }

            if (sortColumn == "CustomerAddress")
            {
                if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.Streetaddress);
                else
                    query = query.OrderBy(c => c.Streetaddress);

            }

            if (sortColumn == "CustomerCity")
            {
                if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.City);
                else
                    query = query.OrderBy(c => c.City);

            }

           

            return query.GetPaged(page, 10);
        }
    }

}
