using BankWebApp.Models;
using ConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Controllers
{
    public class TransactionsController
    {
        //private readonly ITransactionService _transactionService;
        //public TransactionsController(ITransactionService transactionService)
        //{
        //    _transactionService = transactionService;
        //}
        private readonly BankContext _context;

        public TransactionsController(BankContext context)
        {
            _context = context;
        }


        // Get transactions and log to console
        //public void ListTransactions()
        //{
        //    //var transactions = _transactionService.GetAll();
        //    var transactions = _context.Transactions.AsQueryable();

        //    foreach (var transaction in transactions)
        //    {
        //        Console.WriteLine(transaction);
        //    };
        //}
    }
}
