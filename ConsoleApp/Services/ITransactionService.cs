using BankWebApp.Models;
using ConsoleApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    public interface ITransactionService
    {
        public IEnumerable<CustomerDTO> CustomersPerCountry(string country);
        public IEnumerable<Transaction> GetTransactionsForAccount(int accountId);

        public DateTime ReadDateFromTextFile();
        public void WriteDateToTextFile();
    }
}
