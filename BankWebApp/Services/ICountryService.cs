using BankWebApp.ViewModels;

namespace BankWebApp.Services
{
    public interface ICountryService
    {
        public IEnumerable<CountryViewModel> GetCountryData();
    }
}
