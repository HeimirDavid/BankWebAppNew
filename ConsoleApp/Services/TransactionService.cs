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
    internal class TransactionService : ITransactionService
    {
        private readonly BankContext _context;
        public TransactionService(BankContext context)
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
                .SelectMany(c => c.Dispositions) //// THIS IS THE ONE I WAS MISSING. TACK RICHARD
                .Select(c => new CustomerDTO
                {
                    CustomerId = c.CustomerId,
                    Country = country,
                    Account = c.Account
                })
                .ToList();

            return customers;

        }

    }
}
