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


            foreach (var country in Countries)
            {
                var query = _context.Customers
                    .Where(c => c.Country == country.CountryName)
                    .Include(a => a.Dispositions)
                    .ThenInclude(d => d.Account).ToList();

                country.Customers = query.Count();
                country.Accounts = query.Sum(c => c.Dispositions.Count(d => d.Type == "OWNER"));
                country.Balance = query.Sum(c => c.Dispositions.Where(d => d.Type == "OWNER").Sum(d => d.Account.Balance));
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
