using BankWebApp.Models;
using BankWebApp.ViewModels;

namespace BankWebApp.Services
{
    public class UserService : IUserService
    {
        private readonly BankContext _context;

        public UserService(BankContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetUsers()
        {
            var query = _context.Users;
            return query;
        }
    }
}
