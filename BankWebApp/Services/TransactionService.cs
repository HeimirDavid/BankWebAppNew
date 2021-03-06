using BankWebApp.Models;
using System.Globalization;
using static BankWebApp.Services.ITransactionService;

namespace BankWebApp.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly BankContext _context;

        public DateTime TodaysDate { get; set; }

        public decimal AmountTooHigh { get; set; } = 15000;


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
            TodaysDate = DateTime.Now;
            var WeekNumAndYear = GetWeek(TodaysDate);

            int WeekNum = WeekNumAndYear.Item1 -2;
            int year = WeekNumAndYear.Item2;

            var weekDates = GetWeekDays(year, WeekNum);
            var FirstDayOfTheWeek = weekDates.First();
            var LastDayOfTheWeek = weekDates.Last();

            var query = _context.Transactions.Where(t => t.Date >= FirstDayOfTheWeek && t.Date <= LastDayOfTheWeek).ToList();

           
            return query;
        }

        public IEnumerable<Transaction> GetThisWeek()
        {
            TodaysDate = DateTime.Now;
            var WeekNumAndYear = GetWeek(TodaysDate);

            int WeekNum = WeekNumAndYear.Item1 - 1;
            int year = WeekNumAndYear.Item2;

            var weekDates = GetWeekDays(year, WeekNum);
            var FirstDayOfTheWeek = weekDates.First();
            var LastDayOfTheWeek = weekDates.Last();

            var query = _context.Transactions.Where(t => t.Date >= FirstDayOfTheWeek && t.Date <= LastDayOfTheWeek).ToList();


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

    }
}
