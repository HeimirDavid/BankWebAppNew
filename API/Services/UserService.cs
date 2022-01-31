using BankWebApp.MobileApiModels;
using BankWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly BankContext _context;

        public UserService(UserManager<User> userManager, BankContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        public CustomerProfileModel GetUserForApi(string userEmail)
        {
            var user = _userManager.Users.First(u => u.UserName == userEmail);

            CustomerProfileModel userProfileModel = new CustomerProfileModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = GetUserRoles(user.Id),
            };

            return userProfileModel;

        }

        private IEnumerable<IdentityRole> GetUserRoles(string userId)
        {
            var user = _userManager.Users.First(u => u.Id == userId);
            var userRoles = _context.UserRoles.Where(ur => ur.UserId == userId);

            var roles = new List<IdentityRole>();

            foreach (var userrole in userRoles)
            {
                var role = _context.Roles.First(r => r.Id == userrole.RoleId);
                roles.Add(role);
            }

            return roles;

        }
    }
}
