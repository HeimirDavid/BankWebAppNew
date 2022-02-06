using AutoMapper;
using BankWebApp.Data;
using BankWebApp.Services;
using BankWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankWebApp.Pages.Customers
{
    [Authorize(Roles = "Cashier,Admin")]
    public class EditModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public EditModel(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }
        [BindProperty]
        public List<SelectListItem> CountryList { get; set; }
        [BindProperty]

        public List<SelectListItem> PhoneCode { get; set; }
        [BindProperty]

        public List<SelectListItem> GenderList { get; set; }
        [BindProperty]

        public EditCustomerViewModel CustomerViewModel { get; set; }

        public CustomerView CustomerToEdit { get; set; }

        // TODO FIX"!!! Why does CustomerViewModel loose its Id on post???
        [BindProperty]
        public int CustomerId { get; set; }

        public void OnGet(int customerId)
        {
            FillCountries();
            FillPhoneCode();
            FillGenderList();

            CustomerToEdit = _customerService.GetCustomer(customerId);

            // TODO Fix wtf???
            var test = _mapper.Map(CustomerToEdit, CustomerViewModel);
            CustomerViewModel = test;
        }

        public IActionResult OnPost(int customerId)
        {
            string Error = "Error";
            CustomerViewModel.CustomerId = customerId;

            if (BankWebApp.Data.Countries._Countries[CustomerViewModel.Country] == Error)
            {
                ModelState.AddModelError("NewCustomer.Country", "Must choose a country");
            }
            else
            {
                CustomerViewModel.CountryCode = BankWebApp.Data.Countries._Countries[CustomerViewModel.Country];
            }

            if (CountryPhoneCode._Codes[CustomerViewModel.Telephonecountrycode] == Error)
            {
                ModelState.AddModelError("NewCustomer.Telephonecountrycode", "Must choose a country code");
            }

            if (GenderDict.Genders[CustomerViewModel.Gender] == Error)
            {
                ModelState.AddModelError("NewCustomer.Gender", "Must choose a gender");
            }

            if (ModelState.IsValid)
            {
                _customerService.UpdateCustomer(CustomerViewModel);
                return RedirectToPage("../Customers/Customer/", new { customerId = CustomerId, edit = true });
            }
            return Page();
            
        }

        private void FillCountries()
        {
            CountryList = BankWebApp.Data.Countries._Countries.Keys.Select(c => new SelectListItem
            {
                Text = c,
                Value = c
            }).ToList();
        }

        private void FillPhoneCode()
        {
            PhoneCode = CountryPhoneCode._Codes.Keys.Select(c => new SelectListItem
            {
                Text = c,
                Value = c
            }).ToList();
        }

        private void FillGenderList()
        {
            GenderList = GenderDict.Genders.Keys.Select(c => new SelectListItem
            {
                Text = c,
                Value = c
            }).ToList();

        }
    }
}
