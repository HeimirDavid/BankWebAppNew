using BankWebApp.Models;
using ConsoleApp.DTO;
using ConsoleApp.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Controllers
{
    public class TransactionsController
    {

        private readonly BankContext _context;
        private readonly ITransactionService _transactionService;

        public TransactionsController(BankContext context, ITransactionService transactionService)
        {
            _context = context;
            _transactionService = transactionService;
        }



        public class SusTransactionsPerCountry
        {
            public IEnumerable<SusSingleTransactionDTO> SusSingle { get; set; }
            public IEnumerable<SusManyTransactionsDTO> SusMany { get; set; }

        }


        public SusTransactionsPerCountry TransactionsPerCountry(string country)
        {
          
            var susTransactionsPerCountry = new SusTransactionsPerCountry();


            susTransactionsPerCountry.SusSingle = GetSusSingleTransactions(country);
            susTransactionsPerCountry.SusMany = GetSusThreeDaysTransactions(country);



            return susTransactionsPerCountry;

        }


        private IEnumerable<SusSingleTransactionDTO> GetSusSingleTransactions(string country)
        {
            var customers = _transactionService.CustomersPerCountry(country);
            //var accounts = customers.Select(c => c.Account);


            var susSingleTransactions = customers
                .Select(c => c.Account)
                .SelectMany(a => a.Transactions)
                .OrderBy(t => t.Date)
                .Where(t => t.Amount > 15000)
                .Select(t => new SusSingleTransactionDTO
                {
                    TransactionId = t.TransactionId,
                    AccountId = t.AccountId,
                    Amount = t.Amount,
                    Date = t.Date,

                });
                
            //var susSingleTransactions = new List<SusSingleTransactionDTO>();

            

            //SINGLE FOREACH
            //foreach (var account in accounts)
            //{
            //    foreach (var transaction in account.Transactions.OrderBy(t => t.Date))
            //    {
            //        if (transaction.Amount > 15000)
            //        {
            //            susSingleTransactions.Add(new SusSingleTransactionDTO
            //            {
            //                TransactionId = transaction.TransactionId,
            //                AccountId = transaction.AccountId,
            //                Amount = transaction.Amount,
            //                Date = transaction.Date,
            //            });
            //        }
            //    }
            //}

            return susSingleTransactions;
        }

        private IEnumerable<SusManyTransactionsDTO> GetSusThreeDaysTransactions(string country)
        {

            var customers = _transactionService.CustomersPerCountry(country);
            var accounts = customers.Select(c => c.Account);
            var susManyTransactions = new List<SusManyTransactionsDTO>();

            //THREE DAYS FOREACH
            foreach (var account in accounts)
            {
                var transactions = _context.Transactions
                    .Where(a => a.AccountId == account.AccountId)
                    .OrderBy(t => t.Date);

                foreach (var transaction in transactions)
                {
                    var dateOfTransaction = transaction.Date;
                    var threeDaysBefore = dateOfTransaction.AddDays(-3);

                    var threeDaysSusTransaction = new List<Transaction>();

                    foreach (var susTransaction in account.Transactions.Where(t => t.Date >= threeDaysBefore && t.Date <= dateOfTransaction))
                    {
                        threeDaysSusTransaction.Add(susTransaction);
                    }


                    decimal total = 0;

                    foreach (var item in threeDaysSusTransaction)
                    {
                        total = +item.Amount;
                    };

                    if (total > 23000)
                    {

                        var sus = threeDaysSusTransaction.Select(t => new SusManyTransactionsDTO
                        {
                            Date = t.Date,
                            Transactions = threeDaysSusTransaction,
                        })
                        .ToList();

                        foreach (var item in sus)
                        {
                            susManyTransactions.Add(new SusManyTransactionsDTO
                            {
                                Date = item.Date,
                                Transactions = item.Transactions,
                            });
                        }



                    }

                }

            }

            return susManyTransactions;
        }
    }
}
