using BankWebApp.Models;
using BankWebApp.ViewModels;
using Microsoft.EntityFrameworkCore;

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

        public AccountTransactionsView GetAccountAndTransactions(int accountId, int customerId)
        {
            var query = _context.Accounts.Include(a => a.Transactions).First(a => a.AccountId == accountId);
            var CustomerQuery = _context.Customers.First(c => c.CustomerId == customerId);
            var Item = new AccountTransactionsView
            {
                CustomerName = CustomerQuery.Givenname,
                AccountId = query.AccountId,
                Created = query.Created,
                Balance = query.Balance,
                Transactions = query.Transactions.OrderByDescending(t => t.Date),
            };

            return Item;
        }

        
    }
}
