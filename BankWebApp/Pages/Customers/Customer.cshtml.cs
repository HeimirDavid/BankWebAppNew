using BankWebApp.Services;
using BankWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankWebApp.Pages.Customers
{
    public class CustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;
        public string DepositSuccess { get; set; }
        public string WithdrawSuccess { get; set; }
        public string EditSuccess { get; set; }
        public CustomerModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public CustomerView Customer { get; set; }

        public void OnGet(int customerId, bool deposit, bool withdraw, bool edit)
        {
            Customer = _customerService.GetCustomer(customerId);
            if (deposit)
            {
                DepositSuccess = "The deposit was successful";
            }
            if (withdraw)
            {
                DepositSuccess = "The withdraw was successful";
            }
            if (edit)
            {
                EditSuccess = "Customer updated!";
            }
            
        }
    }
}
