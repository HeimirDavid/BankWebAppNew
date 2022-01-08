using BankWebApp.Models;
using BankWebApp.Services;
using BankWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;

namespace BankWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IUserService _userService;
        private readonly ITransactionService _transactionService;

        public IndexModel(ILogger<IndexModel> logger, IUserService userService, ITransactionService transactionService)
        {
            _logger = logger;
            _userService = userService;
            _transactionService = transactionService;
        }

        public List<IndexUser> Users { get; set; }
        public DateTime TodaysDate { get; set; }

        DateTime date1 = DateTime.Now;
        public TransactionPerWeekDay ThisWeek { get; set; }
        public TransactionPerWeekDay LastWeek { get; set; }



        //public int[] WeeklyTransactionsArray = new int[7];
        public class TransactionPerWeekDay
        {
            public int Mon { get; set; }
            public int Tue { get; set; }
            public int Wed { get; set; }
            public int Thu { get; set; }
            public int Fri { get; set; }
            public int Sat { get; set; }
            public int Sun { get; set; }
        }

        public void OnGet()
        {
            var query = _userService.GetUsers();

            ThisWeek = GetThisWeeksTransactions();
            LastWeek = GetLastWeeksTransactions();

            Users = query.Select(u => new IndexUser
            {
                Id = u.UserId,
                Username = u.LoginName,
            }).Take(5).ToList();


        }



        public TransactionPerWeekDay GetLastWeeksTransactions()
        {
            var Transactions = _transactionService.GetLastWeek();

            var numberOfTransactions = new List<int>();

            var PreviousDate = new DateTime();

            var FirstDate = Transactions.ToList().First().Date;

            TransactionPerWeekDay transactionPerWeekDay = new TransactionPerWeekDay();

            foreach (var t in Transactions)
            {
                if ((int)t.Date.DayOfWeek == 1)
                    transactionPerWeekDay.Mon++;

                if ((int)t.Date.DayOfWeek == 2)
                    transactionPerWeekDay.Tue++;

                if ((int)t.Date.DayOfWeek == 3)
                    transactionPerWeekDay.Wed++;

                if ((int)t.Date.DayOfWeek == 4)
                    transactionPerWeekDay.Thu++;

                if ((int)t.Date.DayOfWeek == 5)
                    transactionPerWeekDay.Fri++;

                if ((int)t.Date.DayOfWeek == 6)
                    transactionPerWeekDay.Sat++;

                if ((int)t.Date.DayOfWeek == 7)
                    transactionPerWeekDay.Sun++;
            }


            //foreach (var t in Transactions)
            //{
            //    if ((int)t.Date.DayOfWeek == 1)
            //       WeeklyTransactionsArray.[0]++;

            //    if ((int)t.Date.DayOfWeek == 2)
            //        transactionPerWeekDay.Tue++;

            //    if ((int)t.Date.DayOfWeek == 3)
            //        transactionPerWeekDay.Wed++;

            //    if ((int)t.Date.DayOfWeek == 4)
            //        transactionPerWeekDay.Thu++;

            //    if ((int)t.Date.DayOfWeek == 5)
            //        transactionPerWeekDay.Fri++;

            //    if ((int)t.Date.DayOfWeek == 6)
            //        transactionPerWeekDay.Sat++;

            //    if ((int)t.Date.DayOfWeek == 7)
            //        transactionPerWeekDay.Sun++;
            //}




            return transactionPerWeekDay;

        }



        public TransactionPerWeekDay GetThisWeeksTransactions()
        {
            var Transactions = _transactionService.GetThisWeek();

            var numberOfTransactions = new List<int>();

            var PreviousDate = new DateTime();

            var FirstDate = Transactions.ToList().First().Date;

            TransactionPerWeekDay transactionPerWeekDay = new TransactionPerWeekDay();

            foreach (var t in Transactions)
            {
                if ((int)t.Date.DayOfWeek == 1)
                    transactionPerWeekDay.Mon++;

                if ((int)t.Date.DayOfWeek == 2)
                    transactionPerWeekDay.Tue++;

                if ((int)t.Date.DayOfWeek == 3)
                    transactionPerWeekDay.Wed++;

                if ((int)t.Date.DayOfWeek == 4)
                    transactionPerWeekDay.Thu++;

                if ((int)t.Date.DayOfWeek == 5)
                    transactionPerWeekDay.Fri++;

                if ((int)t.Date.DayOfWeek == 6)
                    transactionPerWeekDay.Sat++;

                if ((int)t.Date.DayOfWeek == 7)
                    transactionPerWeekDay.Sun++;
            }

            return transactionPerWeekDay;
        }
    }
}