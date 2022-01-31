using BankWebApp.Data;
using BankWebApp.Services;
using BankWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Globalization;


namespace BankWebApp.Pages.Customers
{
    [Authorize(Roles = "Cashier,Admin")]
    [BindProperties]
    public class NewModel : PageModel
    {
        private readonly ICustomerService _customerService;
        public NewModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Required]
        public NewCustomerViewModel NewCustomer { get; set; }


        [Required]
        public List<SelectListItem> CountryList { get; set; }


        [Required]
        public List<SelectListItem> PhoneCode { get; set; }

        [Required]
        public List<SelectListItem> GenderList { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        // TODO Chech if necessary
        [Required]
        public List<SelectListItem> Gender { get; set; }
        public void OnGet()
        {
            FillCountries();
            FillPhoneCode();
            FillGenderList();
        }

        public IActionResult OnPost()
        {
            FillCountries();
            FillPhoneCode();
            FillGenderList();

            string Error = "Error";

            if (BankWebApp.Data.Countries._Countries[NewCustomer.Country] == Error)
            {
                ModelState.AddModelError("NewCustomer.Country", "Must choose a country");
            } else
            {
                NewCustomer.CountryCode = BankWebApp.Data.Countries._Countries[NewCustomer.Country]; 
            }

            if (CountryPhoneCode._Codes[NewCustomer.Telephonecountrycode] == Error)
            {
                ModelState.AddModelError("NewCustomer.Telephonecountrycode", "Must choose a country code");
            }

            if (GenderDict.Genders[NewCustomer.Gender] == Error)
            {
                ModelState.AddModelError("NewCustomer.Gender", "Must choose a gender");
            }




            if (ModelState.IsValid)
            {
                _customerService.AddCustomer(NewCustomer, Password, ConfirmPassword);
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

        //protected void Page_Load()

        //{

        //    CultureInfo[] cinfo = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);

        //    Response.WriteAsync("<table border=\"1\"><tr><th>Country Name</th><th>Language-Country code</th></tr>");

        //    foreach (CultureInfo cul in cinfo)

        //    {

        //        Response.WriteAsync("<tr>");

        //        Response.WriteAsync("<td>" + cul.DisplayName + " </td><td> " + cul.TwoLetterISOLanguageName + "</td>");

        //        Response.WriteAsync("</tr>");

        //    }

        //    Response.WriteAsync("</table>");

        //}
    }
}
