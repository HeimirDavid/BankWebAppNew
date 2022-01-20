using BankWebApp.Services;
using BankWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BankWebApp.Pages.Transactions
{
    public class WithdrawModel : PageModel
    {
        private readonly ITransactionService _transactionService;
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;
        public WithdrawModel(ITransactionService transactionService, ICustomerService customerService, IAccountService accountService)
        {
            _transactionService = transactionService;
            _customerService = customerService;
            _accountService = accountService;
        }
        //public DateTime DateWhen { get; set; }
        [BindProperty]
        [Range(10, 15000)]
        public decimal Amount { get; set; }

        public AccountViewModel Account { get; set; }

        public void OnGet(int accountId, int customerId)
        {
            Account = _accountService.GetAccount(accountId, customerId);
            Amount = 0;
        }

        public IActionResult OnPost(int accountId, int customerId)
        {

            Account = _accountService.GetAccount(accountId, customerId);

            if (ModelState.IsValid)
            {
                var Customer = _customerService.GetCustomerWithAccountNo(accountId);
                var status = _transactionService.Withdraw(accountId, Amount);
                if (status == ITransactionService.TransactionError.Ok)
                {
                    return RedirectToPage("../Customers/Customer/", new { customerId = Customer.CustomerId, Withdraw = true });
                }
                else if (status == ITransactionService.TransactionError.AmountTooHigh)
                {
                    ModelState.AddModelError("Amount", "AmountToHigh");
                    return Page();
                }
                else if (status == ITransactionService.TransactionError.InvalidAccount)
                {
                    ModelState.AddModelError("Account", "Invalid Account");
                    return Page();
                }
                else if (status == ITransactionService.TransactionError.BalanceTooLow)
                {
                    ModelState.AddModelError("Amount", "Not enough balance");
                    return Page();
                }

            }

            return Page();

        }
    }
}
