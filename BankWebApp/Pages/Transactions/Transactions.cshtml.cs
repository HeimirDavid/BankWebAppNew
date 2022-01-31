using BankWebApp.Models;
using BankWebApp.Services;
using BankWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BankWebApp.Pages.Transactions
{
    [Authorize(Roles = "Cashier,Admin")]
    public class TransactionsModel : PageModel
    {
        private readonly ITransactionService _transactionService;
        private readonly ICustomerService _customerService;
        public TransactionsModel(ITransactionService transactionService, ICustomerService customerService)
        {
            _transactionService = transactionService;
            _customerService = customerService;
        }
        [BindProperty]
        [Required]
        [Range(1, 15000)]
        public decimal Amount { get; set; }
        [BindProperty]
        [Required]
        public int AccountNoFrom { get; set; }
        //[NotEqualTo("AccountNoFrom")]
        // https://stackoverflow.com/questions/8786251/opposite-of-compare-data-annotation-in-net
        [BindProperty]
        [Required]
        public int AccountNoTo { get; set; }
        public string TransactionMessage { get; set; }
        public List<TransactionDropdown> TransactionsDropDown { get; set; }
        public void OnGet(bool TransactionSuccess)
        {
            var Dispositions = _customerService.GetAllDispositions();

            TransactionsDropDown = FillDropdownWithAccounts();

            if (TransactionSuccess)
            {
                TransactionMessage = "Transfer was successful!";
            }
        }

        public IActionResult OnPost()
        {

            TransactionsDropDown = FillDropdownWithAccounts();

            if (ModelState.IsValid)
            {
                bool depositSuccessful = false;
                bool withdrawSuccessful = false;

                //Should be changed out later... Following code checking if the two accounts are the same..
                if (AccountNoTo == AccountNoFrom)
                {
                    ModelState.AddModelError("AccountNoFrom", "Can't transfer between the same accounts..");
                    return Page();
                }


                var statusDeposit = _transactionService.Deposit(AccountNoTo, Amount);
                if (statusDeposit == ITransactionService.TransactionError.Ok)
                {
                    depositSuccessful = true;
                }
                else if (statusDeposit == ITransactionService.TransactionError.AmountTooHigh)
                {
                    ModelState.AddModelError("Amount", "AmountToHigh");
                    return Page();
                }
                else if (statusDeposit == ITransactionService.TransactionError.InvalidAccount)
                {
                    ModelState.AddModelError("AccountNoFrom", "Invalid Account");
                    return Page();
                }
                else if (statusDeposit == ITransactionService.TransactionError.InvalidDate)
                {
                    ModelState.AddModelError("DateWhen", "Date must be tomorrow or after");
                    return Page();
                }


                var statusWirhdraw = _transactionService.Withdraw(AccountNoFrom, Amount);
                if (statusWirhdraw == ITransactionService.TransactionError.Ok)
                {
                    withdrawSuccessful = true;
                }
                else if (statusWirhdraw == ITransactionService.TransactionError.AmountTooHigh)
                {
                    ModelState.AddModelError("Amount", "AmountToHigh");
                    return Page();
                }
                else if (statusWirhdraw == ITransactionService.TransactionError.InvalidAccount)
                {
                    ModelState.AddModelError("AccountNoTo", "Invalid Account");
                    return Page();
                }
                else if (statusWirhdraw == ITransactionService.TransactionError.BalanceTooLow)
                {
                    ModelState.AddModelError("Amount", "Not enough balance");
                    return Page();
                }



                if (depositSuccessful && withdrawSuccessful)
                {
                    return RedirectToPage("../Transactions/Transactions", new { TransactionSuccess = true });
                }

            }

            return Page();
        }

        public List<TransactionDropdown> FillDropdownWithAccounts()
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
            return TransactionsDropDown;
        }
    }
}
