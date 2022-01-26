using BankWebApp.Infrastructure.Paging;
using BankWebApp.Models;
using BankWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace BankWebApp.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();

        IEnumerable<IdentityRole> GetRoles();

        public PagedResult<IdentityUser> GetAll(int page, string sortColumn, string sortOrder, string searchWord);

        public IEnumerable<UsersViewModel> GetAllUsersWithRoles(int page, string sortColumn, string sortOrder, string searchWord);

        public UserViewModel GetUser(string userId);
        public EditUserViewModel EditUserOnGet(string userId);
        public Task EditUser(EditUserViewModel editUserViewModel);

        public IEnumerable<IdentityRole> GetUserRoles(string userId);

    }
}
