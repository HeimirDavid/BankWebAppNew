using BankWebApp.Infrastructure.Paging;
using BankWebApp.Models;
using BankWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BankWebApp.Services
{
    public class UserService : IUserService
    {
        private readonly BankContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(BankContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public PagedResult<IdentityUser> GetAll(int page, string sortColumn, string sortOrder, string searchWord)
        {
            //List<UsersViewModel> usersViewModel = new List<UsersViewModel>();
            var query = _userManager.Users;

            if (!string.IsNullOrEmpty(searchWord))
            {
                query = query.Where(c => c.UserName.Contains(searchWord) || c.Email.Contains(searchWord) || c.PhoneNumber.Contains(searchWord) || Convert.ToString(c.Id).Contains(searchWord));
            }

            if (string.IsNullOrEmpty(sortColumn))
            {
                sortColumn = "UserId";
            }
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "asc";
            }

            if (sortColumn == "UserId")
            {
                if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.Id);
                else
                    query = query.OrderBy(c => c.Id);
            }

            if (sortColumn == "UserName")
            {
                if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.UserName);
                else
                    query = query.OrderBy(c => c.UserName);
            }

            

            //return View(usersWithRoles);

            return query.GetPaged(page, 10);

        }

        public IEnumerable<UsersViewModel> GetAllUsersWithRoles(int page, string sortColumn, string sortOrder, string searchWord)
        {

            var users = GetAll(page, sortColumn, sortOrder, searchWord);
            var pageCount = users.PageCount;

            List<UsersViewModel> usersViewModel = new List<UsersViewModel>();

            foreach (var user in users.Results)
            {
                var UserRoles = _context.UserRoles.Where(i => i.UserId == user.Id).ToList();

                List<IdentityRole> roles = new List<IdentityRole>();

                foreach (var userRole in UserRoles)
                {
                    var role = _context.Roles.First(r => r.Id == userRole.RoleId);
                    roles.Add(role);
                }


                usersViewModel.Add(new UsersViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    Roles = roles,
                    PageCount = pageCount,
                });

            }

            return usersViewModel;

        }

        public IEnumerable<IdentityRole> GetRoles()
        {
            var rolesDB = _context.Roles;
            var roles = _roleManager.Roles;
            return roles;
            
        }

        public IEnumerable<User> GetUsers()
        {
            var query = _context.Users;
            return query;
        }




    }
}
