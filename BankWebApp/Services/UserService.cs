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

        public UserService(BankContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
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
