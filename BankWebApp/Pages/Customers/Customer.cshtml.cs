using BankWebApp.Services;
using BankWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankWebApp.Pages.Customers
{
    public class CustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CustomerModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public CustomerView Customer { get; set; }

        public void OnGet(int customerId)
        {
            Customer = _customerService.GetCustomer(customerId);
        }
    }
}
