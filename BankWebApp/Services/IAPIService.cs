using BankWebApp.Models;

namespace BankWebApp.Services
{
    public interface IAPIService
    {
        public User GetCurrentUser();
    }
}
