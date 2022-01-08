using BankWebApp.Models;
using System.Globalization;

namespace BankWebApp.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly BankContext _context;

        public DateTime TodaysDate { get; set; }

        DateTime date1 = new DateTime(2013, 1, 15);


        enum Season
        {
            FirstDay = 1,
            Sunday = 0,
        }

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

            int WeekNum = WeekNumAndYear.Item1 -1;
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

            int WeekNum = WeekNumAndYear.Item1;
            int year = WeekNumAndYear.Item2;

            var weekDates = GetWeekDays(year, WeekNum);
            var FirstDayOfTheWeek = weekDates.First();
            var LastDayOfTheWeek = weekDates.Last();

            var query = _context.Transactions.Where(t => t.Date >= FirstDayOfTheWeek && t.Date <= LastDayOfTheWeek);


            return query;
        }
    }
}
