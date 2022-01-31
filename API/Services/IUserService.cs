using BankWebApp.MobileApiModels;

namespace API.Services
{
    public interface IUserService
    {
        public CustomerProfileModel GetUserForApi(string userEmail);
    }
}
