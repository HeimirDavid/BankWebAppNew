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
        public void OnGet(int accountId, int customerId)
        {
            AccountAndTransactions = _accountService.GetAccountAndTransactions(accountId, customerId);
        }
    }
}
