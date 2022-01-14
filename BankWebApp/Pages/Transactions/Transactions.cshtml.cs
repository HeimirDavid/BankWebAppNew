using BankWebApp.Models;
using BankWebApp.Services;
using BankWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankWebApp.Pages.Transactions
{
    public class TransactionsModel : PageModel
    {
        private readonly ITransactionService _transactionService;
        private readonly ICustomerService _customerService;
        public TransactionsModel(ITransactionService transactionService, ICustomerService customerService)
        {
            _transactionService = transactionService;
            _customerService = customerService;
        }

        public List<TransactionDropdown> TransactionsDropDown { get; set; }
        public void OnGet()
        {
            var Dispositions = _customerService.GetAllDispositions();

            TransactionsDropDown = new List<TransactionDropdown>();

            foreach (var dis in Dispositions)
            {
                TransactionsDropDown.Add(new TransactionDropdown
                {
                    AccountId = dis.AccountId,
                    CustomerName = dis.Customer.Givenname,
                });
            }
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
