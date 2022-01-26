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

            //var query = _context.Customers.Include(c => c.Dispositions).ThenInclude(d => d.Account).ToList();

            CountryViewModel Norway = new CountryViewModel { CountryName = "Norway" };
            CountryViewModel Sweden = new CountryViewModel { CountryName = "Sweden" };
            CountryViewModel Finland = new CountryViewModel { CountryName = "Finland" };
            CountryViewModel Denmark = new CountryViewModel { CountryName = "Denmark" };

            Countries.Add(Norway);
            Countries.Add(Sweden);
            Countries.Add(Finland);
            Countries.Add(Denmark);


            //var CustomersSweden = _context.Customers.Include(c => c.Dispositions).Where(c => c.Country == "Sweden").ToList();
            //var s = _context.Customers.Where(c => c.Country == "Sweden").ToList();
            //var n = _context.Customers.Where(c => c.Country == "Norway").ToList();
            //var d = _context.Customers.Where(c => c.Country == "Denmark").ToList();
            //var f = _context.Customers.Where(c => c.Country == "Finland").ToList();
            //var ice = _context.Customers.Where(c => c.CountryCode == "IS").ToList();

            //var czz = _context.Customers.ToList();
            //var totalOfAccounts = _context.Accounts.ToList();
            //decimal totalBalance = 0;

            //foreach (var acc in totalOfAccounts)
            //{
            //    totalBalance += acc.Balance;
            //}


            //foreach (var customer in CustomersSweden)
            //{
            //    var dispositions = _context.Dispositions.Where(d => d.CustomerId == customer.CustomerId);
            //    foreach (var disposition in dispositions)
            //    {
            //        var accounts = _context.Accounts.Where(a => a.AccountId == disposition.AccountId);
            //    }
            //}

           
            var query = _context.Accounts
                    .Include(a => a.Dispositions).ThenInclude(d => d.Customer);

            foreach (var country in Countries)
            {

                List<Account> ac = new List<Account>();

                foreach (var account in query)
                {
                    bool isFromCountry = false;
                    foreach (var disposition in account.Dispositions)
                    {
                        if (disposition.Customer != null)
                        {
                            if (disposition.Customer.Country == country.CountryName)
                            {
                                //c.Add(new Customer
                                //{
                                //    CustomerId = disposition.CustomerId,
                                //    Country = disposition.Customer.Country,
                                //});

                                country.Customers++;
                                isFromCountry = true;

                            }
                        }

                    }
                    if (isFromCountry)
                    {
                        ac.Add(new Account
                        {
                            AccountId = account.AccountId,
                        });

                        //country.Accounts++;
                        country.Balance += account.Balance;
                    }
                }


                List<Account> AccRemoveDublicates = ac
                    .GroupBy(c => c.AccountId)
                    .Select(c => c.First())
                    .ToList();

                country.Accounts = AccRemoveDublicates.Count();
            }


            ////SWEDEN
            //List<Customer> customers = new List<Customer>();

            //foreach (var account in query)
            //{
            //    foreach (var disposition in account.Dispositions)
            //    {
            //        if (disposition.Customer != null)
            //        {
            //            if (disposition.Customer.Country == "Sweden")
            //            {
            //                customers.Add(new Customer
            //                {
            //                    CustomerId = disposition.CustomerId,
            //                    Country = disposition.Customer.Country,
            //                });
            //            }
            //        }

            //    }
            //}


            //List<Customer> customersOfSweden = customers
            //    .GroupBy(c => c.CustomerId)
            //    .Select(c => c.First())
            //    .ToList();


            //Console.WriteLine("Hejhopp");



            //// NORWAY

            //List<Customer> OriginalNorwayCustomers = new List<Customer>();

            //foreach (var account in query)
            //{
            //    foreach (var disposition in account.Dispositions)
            //    {
            //        if (disposition.Customer != null)
            //        {
            //            if (disposition.Customer.Country == "Norway")
            //            {
            //                OriginalNorwayCustomers.Add(new Customer
            //                {
            //                    CustomerId = disposition.CustomerId,
            //                    Country = disposition.Customer.Country,
            //                });
            //            }
            //        }

            //    }
            //}

            //List<Customer> customersOfNorway = OriginalNorwayCustomers
            //    .GroupBy(c => c.CustomerId)
            //    .Select(c => c.First())
            //    .ToList();






            //// Denmark

            //List<Customer> OriginalDenmarkCustomers = new List<Customer>();

            //foreach (var account in query)
            //{
            //    foreach (var disposition in account.Dispositions)
            //    {
            //        if (disposition.Customer != null)
            //        {
            //            if (disposition.Customer.Country == "Denmark")
            //            {
            //                OriginalDenmarkCustomers.Add(new Customer
            //                {
            //                    CustomerId = disposition.CustomerId,
            //                    Country = disposition.Customer.Country,
            //                });
            //            }
            //        }

            //    }
            //}

            //List<Customer> customersOfDenmark = OriginalDenmarkCustomers
            //    .GroupBy(c => c.CustomerId)
            //    .Select(c => c.First())
            //    .ToList();








            //// Finland

            //List<Customer> OriginalFinlandCustomers = new List<Customer>();

            //foreach (var account in query)
            //{
            //    foreach (var disposition in account.Dispositions)
            //    {
            //        if (disposition.Customer != null)
            //        {
            //            if (disposition.Customer.Country == "Finland")
            //            {
            //                OriginalFinlandCustomers.Add(new Customer
            //                {
            //                    CustomerId = disposition.CustomerId,
            //                    Country = disposition.Customer.Country,
            //                });
            //            }
            //        }

            //    }
            //}

            //List<Customer> customersOfFinland = OriginalFinlandCustomers
            //    .GroupBy(c => c.CustomerId)
            //    .Select(c => c.First())
            //    .ToList();



            return Countries;
        }
    }
}
