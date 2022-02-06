using BankWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    public interface ITransactionService
    {
        public IEnumerable<Transaction> GetAll();
    }
}
