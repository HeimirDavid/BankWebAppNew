using BankWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankWebApp.Pages.Transactions
{
    public class TransactionsModel : PageModel
    {
        private readonly ITransactionService _transactionService;
        public TransactionsModel(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost(int accountId, decimal amount)
        {
            if (ModelState.IsValid)
            {
                var status = _transactionService.Withdraw(accountId, amount);
                if (status == ITransactionService.TransactionError.Ok)
                {
                    return RedirectToPage("Index");
                }
                else if (status == ITransactionService.TransactionError.BalanceTooLow)
                {
                    ModelState.AddModelError("Amount", "Error with the amount");
                    return Page();
                }
                else if (status == ITransactionService.TransactionError.InvalidAccount)
                {
                    ModelState.AddModelError("Account", "Invalid Account");
                    return Page();
                }

            }

            return Page();
        }
    }
}
