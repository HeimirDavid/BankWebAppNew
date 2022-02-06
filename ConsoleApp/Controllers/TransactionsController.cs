using BankWebApp.Models;
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
        //private readonly ITransactionService _transactionService;
        //public TransactionsController(ITransactionService transactionService)
        //{
        //    _transactionService = transactionService;
        //}
        private readonly BankContext _context;
        private readonly ITransactionService _transactionService;


        public TransactionsController(BankContext context, ITransactionService transactionService)
        {
            _context = context;
            _transactionService = transactionService;
        }


        public class CustomerPerCountryModel
        {
            public int CustomerId { get; set; }
            public string Country { get; set; }

            public Account Account { get; set; }
        }



        public class SusSingleTransaction
        {
            public int AccountId { get; set; }
            public int TransactionId { get; set; }
            public decimal Amount { get; set; }
            public DateTime Date { get; set; }
        }



        public class SusManyTransactions
        {
            public DateTime Date { get; set; }
            public IEnumerable<Transaction> Transactions { get; set; }
        }



        public class SusTransactionsPerCountry
        {
            public IEnumerable<SusSingleTransaction> SusSingle { get; set; }
            public IEnumerable<SusManyTransactions> SusMany { get; set; }

        }




        public IEnumerable<CustomerPerCountryModel> CustomersPerCountry(string country)
        {
            var customers = _context.Customers
                .Include(c => c.Dispositions)
                .ThenInclude(d => d.Account)
                .ThenInclude(a => a.Transactions)
                .Where(c => c.Country == country)
                .SelectMany(c => c.Dispositions) //// THIS IS THE ONE I WAS MISSING. TACK RICHARD
                .Select(c => new CustomerPerCountryModel
                {
                    CustomerId = c.CustomerId,
                    Country = country,
                    Account = c.Account
                })
                .ToList();

            _transactionService.Test();

            return customers;

        }

        public SusTransactionsPerCountry TransactionsPerCountry(string country)
        {
            var customers = CustomersPerCountry(country);
            var accounts = customers.Select(c => c.Account);

            var susSingleTransactions = GetSusSingleTransactions(country);


            var susTransactionsPerCountry = new SusTransactionsPerCountry();

            

           


            susTransactionsPerCountry.SusSingle = susSingleTransactions;
            susTransactionsPerCountry.SusMany = susManyTransactions;



            return susTransactionsPerCountry;

        }


        private IEnumerable<SusSingleTransaction> GetSusSingleTransactions(string country)
        {
            var customers = CustomersPerCountry(country);
            var accounts = customers.Select(c => c.Account);

            var susSingleTransactions = new List<SusSingleTransaction>();

            //SINGLE FOREACH
            foreach (var account in accounts)
            {
                foreach (var transaction in account.Transactions.OrderBy(t => t.Date))
                {
                    if (transaction.Amount > 15000)
                    {
                        susSingleTransactions.Add(new SusSingleTransaction
                        {
                            TransactionId = transaction.TransactionId,
                            AccountId = transaction.AccountId,
                            Amount = transaction.Amount,
                            Date = transaction.Date,
                        });
                    }
                }
            }

            return susSingleTransactions;
        }

        private IEnumerable<SusManyTransactions> GetSusThreeDaysTransactions(string country)
        {

            var customers = CustomersPerCountry(country);
            var accounts = customers.Select(c => c.Account);

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

                        var sus = threeDaysSusTransaction.Select(t => new SusManyTransactions
                        {
                            Date = t.Date,
                            Transactions = threeDaysSusTransaction,
                        })
                        .ToList();

                        foreach (var item in sus)
                        {
                            susManyTransactions.Add(new SusManyTransactions
                            {
                                Date = item.Date,
                                Transactions = item.Transactions,
                            });
                        }



                    }

                }

            }
        }
    }
}
