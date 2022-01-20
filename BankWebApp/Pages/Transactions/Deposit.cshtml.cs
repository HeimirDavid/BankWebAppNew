using BankWebApp.Services;
using BankWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BankWebApp.Pages.Transactions
{
    public class DepositModel : PageModel
    {
        private readonly ITransactionService _transactionService;
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;
        public DepositModel(ITransactionService transactionService, ICustomerService customerService, IAccountService accountService)
        {
            _transactionService = transactionService;
            _customerService = customerService;
            _accountService = accountService;
        }

        [BindProperty]

        [Range(1, 15000)]
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
                var status = _transactionService.Deposit(accountId, Amount);
                if (status == ITransactionService.TransactionError.Ok)
                {
                    
                    return RedirectToPage("../Customers/Customer/", new { customerId = Customer.CustomerId, Deposit = true});
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
                else if (status == ITransactionService.TransactionError.InvalidDate)
                {
                    ModelState.AddModelError("DateWhen", "Date must be tomorrow or after");
                    return Page();
                }

            }

            return Page();

        }
    }
}
