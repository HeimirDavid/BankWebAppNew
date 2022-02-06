using BankWebApp.Models;
using ConsoleApp.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    internal class FraudService : IFraudService
    {
        private readonly BankContext _context;
        public FraudService(BankContext context)
        {
            _context = context;
        }



        public IEnumerable<CustomerDTO> CustomersPerCountry(string country)
        {
            var customers = _context.Customers
                .Include(c => c.Dispositions)
                .ThenInclude(d => d.Account)
                .ThenInclude(a => a.Transactions)
                .Where(c => c.Country == country)
                .SelectMany(c => c.Dispositions)
                .Select(c => new CustomerDTO
                {
                    CustomerId = c.CustomerId,
                    Country = country,
                    Account = c.Account
                })
                .ToList();

            return customers;

        }

        public IEnumerable<Transaction> GetTransactionsForAccount(int accountId)
        {
            var date = ReadDateFromTextFile();

           var accounts = _context.Transactions
                .Where(a => a.AccountId == accountId)
                .Where(a => a.Date > date)
                .OrderBy(t => t.Date);

            return accounts;
        }

        public DateTime ReadDateFromTextFile()
        {
            string d = File.ReadAllText("../../../Reports/LastRun.txt");
            var LastRunDate = DateTime.Parse(d);

            return LastRunDate;
        }

        public void WriteDateToTextFile()
        {
            string path = "../../../Reports/LastRun.txt";
            string d = DateTime.Now.Date.ToString();
            //var LastRunDate = DateTime.Parse(d);
            File.WriteAllText(path, d);
        }
    }
}
