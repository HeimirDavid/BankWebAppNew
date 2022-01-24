using BankWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace BankWebApp.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();

        IEnumerable<IdentityRole> GetRoles();
    }
}
