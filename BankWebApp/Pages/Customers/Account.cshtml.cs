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

        public AccountViewModel Account { get; set; }
        public void OnGet(int accountId, int customerId)
        {
            Account = _accountService.GetAccount(accountId, customerId);
        }

        public IActionResult OnGetFetchMore(int accountId, int pageNo)
        {
  
            var transactions = _accountService.GetAllTransactions(accountId, pageNo);

            return new JsonResult(new { items = transactions, pageNo });

        }
    }
}
