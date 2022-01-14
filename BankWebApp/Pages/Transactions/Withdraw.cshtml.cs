using BankWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BankWebApp.Pages.Transactions
{
    [BindProperties]
    public class WithdrawModel : PageModel
    {
        private readonly ITransactionService _transactionService;
        private readonly ICustomerService _customerService;
        public WithdrawModel(ITransactionService transactionService, ICustomerService customerService)
        {
            _transactionService = transactionService;
            _customerService = customerService;
        }
        //public DateTime DateWhen { get; set; }

        [Range(10, 15000)]
        public decimal Amount { get; set; }


        public void OnGet(int accountId)
        {
            //DateWhen = DateTime.Now.AddDays(1).Date;
            Amount = 0;
        }

        public IActionResult OnPost(int accountId)
        {
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
