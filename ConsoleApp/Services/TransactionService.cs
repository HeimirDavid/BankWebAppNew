using BankWebApp.Models;
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

        
      
    }
}
