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

        private readonly IFraudService _transactionService;

        public TransactionsController(IFraudService transactionService)
        {
            _transactionService = transactionService;
        }


        public SusTransactionsPerCountryDTO TransactionsPerCountry(string country)
        {
          
            var susTransactionsPerCountry = new SusTransactionsPerCountryDTO();


            susTransactionsPerCountry.SusSingle = GetSusSingleTransactions(country);
            susTransactionsPerCountry.SusMany = GetSusThreeDaysTransactions(country);

            return susTransactionsPerCountry;

        }

        /// TA IN ETT DATUM
        private IEnumerable<SusSingleTransactionDTO> GetSusSingleTransactions(string country)
        {
            var customers = _transactionService.CustomersPerCountry(country);

            var dateLastRun = _transactionService.ReadDateFromTextFile();

            var susSingleTransactions = customers
                .Select(c => c.Account)
                .SelectMany(a => a.Transactions)
                .Where(t => t.Date > dateLastRun)
                .OrderBy(t => t.Date)
                .Where(t => t.Amount > 15000)
                .Select(t => new SusSingleTransactionDTO
                {
                    TransactionId = t.TransactionId,
                    AccountId = t.AccountId,
                    Amount = t.Amount,
                    Date = t.Date,

                });

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
                var transactions = _transactionService.GetTransactionsForAccount(account.AccountId);
                
                foreach (var transaction in transactions)
                {
                    //Transaction dates for comparison
                    var dateOfTransaction = transaction.Date;
                    var threeDaysBefore = dateOfTransaction.AddDays(-3);

                    //List to fill with transactions over the past 3 days
                    var threeDaysSusTransaction = new List<Transaction>();

                    foreach (var susTransaction in account.Transactions.Where(t => t.Date >= threeDaysBefore && t.Date <= dateOfTransaction))
                    {
                        threeDaysSusTransaction.Add(susTransaction);
                    }

                    //Loop through the 3 days transactions and check the total. If it is over certain rule (23000), add to suspicious list
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
