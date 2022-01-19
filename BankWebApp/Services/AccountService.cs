using AutoMapper;
using BankWebApp.Infrastructure.Paging;
using BankWebApp.Models;
using BankWebApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BankWebApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly BankContext _context;
        private readonly IMapper _mapper;
        public AccountService(BankContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public int NumberOfAllAccounts()
        {
            int numberOfAccounts = _context.Accounts.Count();
            return numberOfAccounts;
        }

        /* NOT IN USE ANYMORE */
        public AccountTransactionsView GetAccountAndTransactions(int accountId, int customerId, long lastTicks)
        {
            DateTime dateOfLastShown = new DateTime(lastTicks).AddMilliseconds(100);

            var query = _context.Accounts
                .Include(a => a.Transactions)
                .First(a => a.AccountId == accountId);

            var CustomerQuery = _context.Customers.First(c => c.CustomerId == customerId);
            var Item = _mapper.Map<AccountTransactionsView>(query);
            Item.Givenname = CustomerQuery.Givenname;
            Item.Transactions = query.Transactions
                .Where(t => lastTicks == 0 || t.Date > dateOfLastShown)
                .Take(5)
                .OrderByDescending(t => t.Date);

            return Item;
        }

        public AccountViewModel GetAccount(int accountId, int customerId)
        {
            var query = _context.Accounts
                .First(a => a.AccountId == accountId);

            var CustomerQuery = _context.Customers
                .First(c => c.CustomerId == customerId);

            var Item = new AccountViewModel
            {
                Givenname = CustomerQuery.Givenname,
                AccountId = query.AccountId,
                Created = query.Created,
                Balance = query.Balance,
            };

            return Item;
        }

        public IEnumerable<TransactionViewModel> GetAllTransactions(int accountId, int pageNo)
        {

            var transactions = _context.Transactions
                .Where(t => t.AccountId == accountId)
                .OrderByDescending(e => e.Date)
                .GetPaged(pageNo, 20).Results;

            var items = _mapper.Map<IEnumerable<TransactionViewModel>>(transactions);

            return items;
        }



    }
}
