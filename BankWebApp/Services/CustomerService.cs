using BankWebApp.Infrastructure.Paging;
using BankWebApp.Models;
using BankWebApp.ViewModels;
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
            var query = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(searchWord))
            {
                query = query.Where(c => c.Givenname.Contains(searchWord) || c.City.Contains(searchWord) || Convert.ToString(c.CustomerId).Contains(searchWord));
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
        public CustomerView GetCustomer(int id)
        {
            var query = _context.Customers.Include(c => c.Dispositions).ThenInclude(d => d.Account).First(c => c.CustomerId == id);

            var accounts = query.Dispositions.Select(a => a.Account);

            var disposition = query.Dispositions;

            CustomerView Item = new CustomerView
            {
                Id = query.CustomerId,
                Gender = query.Gender,
                Givenname = query.Givenname,
                Surname = query.Surname,
                Streetaddress = query.Streetaddress,
                City = query.City,
                Zipcode = query.Zipcode,
                Country = query.Country,
                CountryCode = query.CountryCode,
                Birthday = query.Birthday,
                NationalId = query.NationalId,
                Telephonecountrycode = query.Telephonecountrycode,
                Telephonenumber = query.Telephonenumber,
                Emailaddress = query.Emailaddress,
                Dispositions = disposition,
                Accounts = accounts,
            };

            return Item;
        }
}

}
