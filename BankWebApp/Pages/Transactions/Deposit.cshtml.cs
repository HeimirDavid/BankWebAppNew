using BankWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankWebApp.Pages.Transactions
{
    public class DepositModel : PageModel
    {
        private readonly ITransactionService _transactionService;
        public DepositModel(ITransactionService transactionService)
        {
            _transactionService = transactionService;


        }
        public void OnGet()
        {
        }
    }
}
