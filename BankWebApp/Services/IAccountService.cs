using BankWebApp.Infrastructure.Paging;
using BankWebApp.Models;
using BankWebApp.ViewModels;

namespace BankWebApp.Services
{
    public interface IAccountService
    {
        public int NumberOfAllAccounts();

        public AccountTransactionsView GetAccountAndTransactions(int accountId, int customerId, long lastTicks);
        public AccountViewModel GetAccount(int accountId, int customerId);
        public IEnumerable<TransactionViewModel> GetAllTransactions(int accountId, int pageNo);
    }
}
