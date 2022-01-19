using BankWebApp.Services;
using BankWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankWebApp.Pages.Customers
{


    public class AccountModel : PageModel
    {
        private readonly IAccountService _accountService;

        public AccountModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public AccountTransactionsView AccountAndTransactions { get; set; }
        public AccountViewModel Account { get; set; }
        public void OnGet(int accountId, int customerId)
        {
            Account = _accountService.GetAccount(accountId, customerId);
        }

        public IActionResult OnGetFetchMore(int accountId, long lastTicks)
        {
            DateTime dateOfLastShown = new DateTime(lastTicks).AddMilliseconds(100);

            var transactions = _accountService.GetAllTransactions(accountId, lastTicks);

            if (transactions.Any())
            {
                lastTicks = transactions.Last().Date.Ticks;
            }

            return new JsonResult(new { items = transactions, lastTicks });
            //var list = _context.Person
            //    .Where(e => e.Id == personId)
            //    .SelectMany(e => e.OwnedCars)
            //    .Where(d => lastTicks == 0 || d.BoughtDate > dateOfLastShown)
            //    .OrderBy(e => e.BoughtDate)
            //    .Take(5)
            //    .Select(e => new Item
            //    {
            //        BoughtDate = e.BoughtDate,
            //        Id = e.Id,
            //        Model = e.Model,
            //        Fuel = e.Fuel,
            //        Manufacturer = e.Manufacturer,
            //        Type = e.Type,
            //        Vin = e.Vin


            //    }).ToList();

            //if (list.Any())
            //    lastTicks = list.Last().BoughtDate.Ticks;
            //return new JsonResult(new { items = list, lastTicks });
        }
    }
}
