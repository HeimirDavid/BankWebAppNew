using AutoMapper;
using BankWebApp.Infrastructure.Paging;
using BankWebApp.Models;
using BankWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BankWebApp.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly BankContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<User> _userStore;
        //private readonly IUserEmailStore<User> _emailStore;

        public CustomerService(BankContext bankContext, 
            IMapper mapper, 
            UserManager<User> userManager,
            IUserStore<User> userStore,
            RoleManager<IdentityRole> roleManager
            )
        {
            _context = bankContext;
            _mapper = mapper;
            _userManager = userManager;
            _userStore = userStore;
            //_emailStore = GetEmailStore();
            _roleManager = roleManager;
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



        public Customer GetCustomerWithAccountNo(int accountId)
        {
            var query = _context.Dispositions.Include(d => d.Customer).First(d => d.AccountId == accountId);
            var customer = query.Customer;

            return customer;
        }

        public IEnumerable<Disposition> GetAllDispositions() {
            var query = _context.Dispositions.Include(d => d.Customer).AsQueryable();

            return query; 
        }

        public async Task AddCustomer(NewCustomerViewModel customer, string password, string confirmPassword)
        {
            // mapp using automapper
            var DBCustomer = _mapper.Map<Customer>(customer);

            //Create a user based on the Customer
            //CODE FROM SEED 
            if (_userManager.FindByEmailAsync(DBCustomer.Emailaddress).Result != null) return;

            var user = new User
            {
                UserName = DBCustomer.Emailaddress,
                Email = DBCustomer.Emailaddress,
                Customer = DBCustomer,
                EmailConfirmed = true,
            };
            string[] roles = new string[] { "Customer" };

            _userManager.CreateAsync(user, password).Wait();
            _userManager.AddToRolesAsync(user, roles).Wait();


            var Account = new Account
            {
                Frequency = "Monthly",
                Created = DateTime.Now,
                Balance = 0,
            };

            _context.Accounts.Add(Account);

            var Disposition = new Disposition
            {
                Customer = DBCustomer,
                Account = Account,
                Type = "OWNER",
            };

            _context.Dispositions.Add(Disposition);

            _context.SaveChanges();
        }

        public void UpdateCustomer(EditCustomerViewModel CustomerWithNewValues)
        {
            var customerFromDb = _context.Customers.First(c => c.CustomerId == CustomerWithNewValues.CustomerId);

            _mapper.Map(CustomerWithNewValues, customerFromDb);
            _context.SaveChanges();
        }



        //private User CreateUser()
        //{
        //    try
        //    {
        //        return Activator.CreateInstance<User>();
        //    }
        //    catch
        //    {
        //        throw new InvalidOperationException($"Can't create an instance of '{nameof(User)}'. " +
        //            $"Ensure that '{nameof(User)}' is not an abstract class and has a parameterless constructor, or alternatively " +
        //            $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
        //    }
        //}
    }

}
