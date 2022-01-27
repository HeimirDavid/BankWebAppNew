using BankWebApp.Models;
using BankWebApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BankWebApp.Services
{
    public class CountryService : ICountryService
    {
        private readonly BankContext _context;
        public CountryService(BankContext context)
        {
            _context = context;
        }

        public class Data
        {
            public int AccountId { get; set; }
            public decimal Balance { get; set; }

            public int CustomerId { get; set; }

            public string CustomerName { get; set; }

            public string CountryName { get; set; }
        }

        public IEnumerable<CountryViewModel> GetCountryData()
        {
            List<CountryViewModel> Countries = new List<CountryViewModel>();

            CountryViewModel Norway = new CountryViewModel { CountryName = "Norway", CountryImage= "images/flags/no.svg" };
            CountryViewModel Sweden = new CountryViewModel { CountryName = "Sweden", CountryImage = "images/flags/se.svg" };
            CountryViewModel Finland = new CountryViewModel { CountryName = "Finland", CountryImage = "images/flags/fi.svg" };
            CountryViewModel Denmark = new CountryViewModel { CountryName = "Denmark", CountryImage = "images/flags/dk.svg" };

            Countries.Add(Norway);
            Countries.Add(Sweden);
            Countries.Add(Finland);
            Countries.Add(Denmark);


            var query = _context.Accounts
                    .Include(a => a.Dispositions)
                    .ThenInclude(d => d.Customer);

            List<Data> accountData = new List<Data>();

            foreach (var country in Countries)
            {

                foreach (var account in query)
                {

                    foreach (var disposition in account.Dispositions)
                    {
                        if (disposition.Customer != null)
                        {
                            if (disposition.Customer.Country == country.CountryName)
                            {
                                accountData.Add(new Data
                                {
                                    AccountId = account.AccountId,
                                    Balance = account.Balance,
                                    CustomerId = disposition.Customer.CustomerId,
                                    CustomerName = disposition.Customer.Givenname,
                                    CountryName = disposition.Customer.Country,
                                });

                                country.Customers++;
                            }
                        }

                    }
                 
                }
            }

            List<Data> DataWithoutDublicates = accountData
                   .GroupBy(c => c.AccountId)
                   .Select(c => c.First())
                   .ToList();

          
            foreach (var country in Countries)
            {
                country.Accounts = DataWithoutDublicates.Where(d => d.CountryName == country.CountryName).Count();
                country.Balance = DataWithoutDublicates.Where(d => d.CountryName == country.CountryName).Sum(d => d.Balance);
            }

            return Countries;
        }


        public IEnumerable<TopCountryViewModel> GetCountryData(string countryName)
        {
            List<TopCountryViewModel> TopCustomers = new List<TopCountryViewModel>();


            TopCustomers = _context.Customers
                .Include(c => c.Dispositions)
                .ThenInclude(d => d.Account)
                .Where(c => c.Country == countryName)
                .SelectMany(c => c.Dispositions) //// THIS IS THE ONE I WAS MISSING. TACK RICHARD
                .OrderByDescending(d => d.Account.Balance)
                .Take(10)
                .Select(d => new TopCountryViewModel
                {
                    CustomerId = d.CustomerId,
                    CustomerName = d.Customer.Givenname,
                    Email = d.Customer.Emailaddress,
                    Balance = d.Account.Balance,
                })
                .ToList();

            return TopCustomers;

        }
    }
}
