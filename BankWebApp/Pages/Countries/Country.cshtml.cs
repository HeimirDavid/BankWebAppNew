using BankWebApp.Services;
using BankWebApp.Data;
using BankWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankWebApp.Pages.Countries
{
    [ResponseCache(Duration = 60, VaryByQueryKeys = new []{"countryName"})]
    public class CountryModel : PageModel
    {
        private readonly ICountryService _countryService;
        public CountryModel(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public IEnumerable<TopCountryViewModel> TopCustomers { get; set; }
        public string CountryFlag { get; set; }
        public string CountryName { get; set; }

        public DateTime ResponseTest { get; set; }

        public void OnGet(string countryName)
        {
            TopCustomers = _countryService.GetCountryData(countryName);
            CountryFlag = BankWebApp.Data.Countries.CountryFlags[countryName];
            CountryName = countryName;
            ResponseTest = DateTime.Now;
        }
    }
}
