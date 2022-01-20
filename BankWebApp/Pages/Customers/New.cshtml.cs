using BankWebApp.Data;
using BankWebApp.Services;
using BankWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Globalization;


namespace BankWebApp.Pages.Customers
{

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

            if (Countries._Countries[NewCustomer.Country] == "Error")
            {
                ModelState.AddModelError("NewCustomer.Country", "Must choose a country");
            }

            if (CountryPhoneCode._Codes[NewCustomer.Telephonecountrycode] == "Error")
            {
                ModelState.AddModelError("NewCustomer.Telephonecountrycode", "Must choose a country code");
            }

            if (GenderDict.Genders[NewCustomer.Gender] == "Error")
            {
                ModelState.AddModelError("NewCustomer.Gender", "Must choose a gender");
            }



            //NewCustomer.CountryCode = CountryPhoneCode._Codes[NewCustomer.Telephonecountrycode];

            if (ModelState.IsValid)
            {
                _customerService.AddCustomer(NewCustomer);
            }


            return Page();
        }

        private void FillCountries()
        {
            CountryList = Countries._Countries.Keys.Select(c => new SelectListItem
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
