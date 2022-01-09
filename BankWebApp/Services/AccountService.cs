using BankWebApp.Models;

namespace BankWebApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly BankContext _context;
        public AccountService(BankContext context)
        {
            _context = context;
        }
        public int NumberOfAllAccounts()
        {
            int numberOfAccounts = _context.Accounts.Count();
            return numberOfAccounts;
        }
    }
}
