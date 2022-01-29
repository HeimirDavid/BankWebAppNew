﻿using BankWebApp.MobileApiModels;
using BankWebApp.Models;
using System.Globalization;
using static BankWebApp.Services.ITransactionService;

namespace BankWebApp.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly BankContext _context;

        public DateTime TodaysDate { get; set; }

        DateTime date1 = DateTime.Now;

        public decimal AmountTooHigh { get; set; } = 15000;

        //enum Season
        //{
        //    FirstDay = 1,
        //    Sunday = 0,
        //}

        public Tuple<int, int> GetWeek(DateTime d)
        {
            CultureInfo cul = CultureInfo.CurrentCulture;

            var firstDayWeek = cul.Calendar.GetWeekOfYear(
                d,
                CalendarWeekRule.FirstDay,
                DayOfWeek.Monday);

            int weekNum = cul.Calendar.GetWeekOfYear(
                d,
                CalendarWeekRule.FirstDay,
                DayOfWeek.Monday);

            int year = weekNum == 52 && d.Month == 1 ? d.Year - 1 : d.Year;
            Console.WriteLine("Year: {0} Week: {1}", year, weekNum);



            return Tuple.Create(weekNum, year);
        }

        public DateTime[] GetWeekDays(int Year, int WeekNumber)
        {
            DateTime start = new DateTime(Year, 1, 1).AddDays(7 * WeekNumber);
            start = start.AddDays(-((int)start.DayOfWeek));
            return Enumerable.Range(0, 7).Select(num => start.AddDays(num)).ToArray();
        }

        public TransactionService(BankContext context)
        {
            _context = context;
        }
        public IEnumerable<Transaction> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetLastWeek()
        {
            TodaysDate = date1;
            var WeekNumAndYear = GetWeek(TodaysDate);

            int WeekNum = WeekNumAndYear.Item1 -2;
            int year = WeekNumAndYear.Item2;

            var weekDates = GetWeekDays(year, WeekNum);
            var FirstDayOfTheWeek = weekDates.First();
            var LastDayOfTheWeek = weekDates.Last();

            var query = _context.Transactions.Where(t => t.Date >= FirstDayOfTheWeek && t.Date <= LastDayOfTheWeek);

           
            return query;
        }

        public IEnumerable<Transaction> GetThisWeek()
        {
            TodaysDate = date1;
            var WeekNumAndYear = GetWeek(TodaysDate);

            int WeekNum = WeekNumAndYear.Item1 - 1;
            int year = WeekNumAndYear.Item2;

            var weekDates = GetWeekDays(year, WeekNum);
            var FirstDayOfTheWeek = weekDates.First();
            var LastDayOfTheWeek = weekDates.Last();

            var query = _context.Transactions.Where(t => t.Date >= FirstDayOfTheWeek && t.Date <= LastDayOfTheWeek);


            return query;
        }

        public TransactionError Deposit(int AccountId, decimal Amount)
        {
            var account = _context.Accounts.First(a => a.AccountId == AccountId);
            if (account == null)
                return TransactionError.InvalidAccount;

            if (Amount > AmountTooHigh)
            {
                return TransactionError.AmountTooHigh;
            }

            account.Balance += Amount;

            _context.Transactions.Add(new Transaction
            {
                AccountId = account.AccountId,
                Date = DateTime.Now,
                Type = "Credit",
                Operation = "Deposit",
                Amount = Amount,
                Balance = account.Balance
            });

            _context.Accounts.Update(account);
            _context.SaveChanges();
            return TransactionError.Ok;
        }

        public TransactionError Withdraw(int AccountId, decimal Amount)
        {
            var account = _context.Accounts.First(a => a.AccountId == AccountId);
            if (account == null)
                return TransactionError.InvalidAccount;

            if (Amount > AmountTooHigh)
            {
                return TransactionError.AmountTooHigh;
            }
            if (Amount > account.Balance)
            {
                return TransactionError.BalanceTooLow;
            }

            account.Balance -= Amount;

            _context.Transactions.Add(new Transaction
            {
                AccountId = account.AccountId,
                Date = DateTime.Now,
                Type = "Debit",
                Operation = "Withdraw",
                Amount = -Amount,
                Balance = account.Balance
            });

            _context.Accounts.Update(account);
            _context.SaveChanges();
            return TransactionError.Ok;
        }

        //public IEnumerable<TransactionsAPIModel> GetTransactions(int AccountId)
        //{
        //    var query = _context.Transactions
                
        //        .Where(a => a.AccountId == AccountId)
        //        .OrderByDescending(a => a.Date)
        //        .Take(10)
        //        .Select(a => new TransactionsAPIModel
        //        {
        //            AccountId = a.AccountId,
        //            TransactionId = a.TransactionId,
        //            Date = a.Date,
        //            Type = a.Type,
        //            Operation = a.Operation,
        //            Amount = a.Amount,
        //            Balance = a.Balance
        //        });


        //    return query;
        //}

        public IEnumerable<TransactionsAPIModel> GetTransactions(int AccountId, int limit, int offset)
        {
            var query = _context.Transactions

                .Where(a => a.AccountId == AccountId)
                .OrderByDescending(a => a.Date)
                .Skip(offset)
                .Take(limit)
                .Select(a => new TransactionsAPIModel
                {
                    AccountId = a.AccountId,
                    TransactionId = a.TransactionId,
                    Date = a.Date,
                    Type = a.Type,
                    Operation = a.Operation,
                    Amount = a.Amount,
                    Balance = a.Balance
                });


            return query;
        }



    }
}
