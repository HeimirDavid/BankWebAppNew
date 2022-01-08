using BankWebApp.Models;

namespace BankWebApp.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
    }
}
