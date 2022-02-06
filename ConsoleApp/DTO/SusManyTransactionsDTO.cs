using BankWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.DTO
{
    public class SusManyTransactionsDTO
    {
        public DateTime Date { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
