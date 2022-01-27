using BankWebApp.ViewModels;

namespace BankWebApp.Services
{
    public interface ICountryService
    {
        public IEnumerable<CountryViewModel> GetCountryData();
        public IEnumerable<TopCountryViewModel> GetCountryData(string countryName);
    }
}
